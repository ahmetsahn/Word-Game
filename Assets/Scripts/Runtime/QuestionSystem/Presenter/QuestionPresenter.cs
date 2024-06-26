using System;
using System.Collections.Generic;
using System.Globalization;
using DG.Tweening;
using Runtime.QuestionSystem.Model;
using Runtime.QuestionSystem.View;
using Runtime.Signals;
using UnityEngine;
using Zenject;

namespace Runtime.QuestionSystem.Presenter
{
    public class QuestionPresenter : IDisposable
    {
        private readonly QuestionView _view;
        
        private readonly QuestionModel _model;
        
        private readonly SignalBus _signalBus;
        
        public QuestionPresenter(
            QuestionView view, 
            QuestionModel model,
            SignalBus signalBus)
        {
            _view = view;
            _model = model;
            _signalBus = signalBus;
            
            Initialize();
            SubscribeEvents();
        }
        
        private void Initialize()
        {
            _model.CurrentQuestionSo = _model.SelectedQuestions[_model.QuestionIndex];
            _view.SetNewQuestionText(_model.CurrentQuestionSo.questionText);
        }
        
        private void SubscribeEvents()
        {
            _signalBus.Subscribe<SendAnswerButtonClickedSignal>(OnSendAnswerButtonClicked);
            _signalBus.Subscribe<CharacterButtonClickedSignal>(OnCharacterButtonClicked);
        }

        private async void OnSendAnswerButtonClicked()
        {
            _signalBus.Fire(new StopTimerSignal());
            
            List<int> emptyIndices = new List<int>();
            for (int i = 0; i < _view.answerTextCharacters.Length; i++)
            {
                if (string.IsNullOrEmpty(_view.answerTextCharacters[i].text))
                {
                    emptyIndices.Add(i);
                }
            }
            
            char currentChar = 'A';
            while (currentChar <= 'Z')
            {
                foreach (int index in emptyIndices)
                {
                    _view.answerTextCharacters[index].text = currentChar.ToString();
                }

                currentChar++;

                await Awaitable.WaitForSecondsAsync(0.05f);
            }
            
            for (int i = 0; i < _model.CurrentQuestionSo.answer.Length; i++)
            {
                _view.answerTextCharacters[i].text = _model.CurrentQuestionSo.answer[i].ToString();
            }

            await Awaitable.WaitForSecondsAsync(2);
            
            _signalBus.Fire(new ResumeTimerSignal());
            
            if(_model.QuestionIndex == _model.SelectedQuestions.Count - 1)
            {
                // TODO: Show final panel
                return;
            }
            
            _model.IncreaseSendAnswerButtonPressCount();
            
            if(string.Equals(_view.answerText.text.ToLower(new CultureInfo("tr-TR")), _model.CurrentQuestionSo.answer.ToLower(new CultureInfo("tr-TR"))))
            {
                var score = _model.CurrentQuestionSo.scoreValue - _model.CharacterButtonPressCount * 100;
                _model.IncreaseScore(score);
            }

            else
            {
                _model.ReduceScore(_model.CurrentQuestionSo.scoreValue);
            }
            
            _view.SetScoreText(_model.Score);
            _view.ResetAnswerCharacterText();
            _view.ResetAnswerText();
            _model.IncreaseQuestionIndex();
            _model.SetNewQuestion();
            _view.SetNewQuestionText(_model.CurrentQuestionSo.questionText);
            _view.SetInteractableCharacterButton(true);
            _model.ResetCharacterButtonPressCount();
            _view.ActivateAnswerTextInputField();
            
            if (_model.SendAnswerButtonPressCount % 2 != 0) return;
            _view.SetActiveAnswerCharacterParentObject(_model.AnswerCharacterParentIndex);
            _model.IncreaseAnswerCharacterParentIndex();
        } 

        private void OnCharacterButtonClicked()
        {
            _view.ActivateAnswerTextInputField();
            
            int randomIndex = UnityEngine.Random.Range(0, _model.CurrentQuestionSo.answer.Length);
            int startIndex = randomIndex;
            
            while (!string.IsNullOrEmpty(_view.answerTextCharacters[randomIndex].text))
            {
                randomIndex = (randomIndex + 1) % _model.CurrentQuestionSo.answer.Length;
                
                if (randomIndex == startIndex)
                {
                    return;
                }
            }
            
            char randomCharacter = _model.CurrentQuestionSo.answer[randomIndex];
            _view.answerTextCharacters[randomIndex].text = randomCharacter.ToString();
            
            _model.CharacterButtonPressCount++;
            
            if (_model.CharacterButtonPressCount >= _model.CurrentQuestionSo.answer.Length)
            {
                _view.SetInteractableCharacterButton(false);
            }
        }
        
        private void UnsubscribeEvents()
        {
            _signalBus.Unsubscribe<SendAnswerButtonClickedSignal>(OnSendAnswerButtonClicked);
            _signalBus.Unsubscribe<CharacterButtonClickedSignal>(OnCharacterButtonClicked);
        }
        
        public void Dispose()
        {
            UnsubscribeEvents();
        }
    }
}