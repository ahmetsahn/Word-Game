using System;
using Runtime.Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Runtime.QuestionSystem.View
{
    public class QuestionView : MonoBehaviour
    {
        public TMP_InputField answerText;
        
        public TextMeshProUGUI[] answerTextCharacters;
        
        [SerializeField]
        private TextMeshProUGUI questionText;
        
        [SerializeField]
        private TextMeshProUGUI scoreText;
        
        [SerializeField]
        private Button sendAnswerButton;
        
        [SerializeField]
        private GameObject[] answerCharacterParentObjects;
        
        [SerializeField]
        private Button characterButton;

        private SignalBus _signalBus;

        private void Start()
        {
            ActivateAnswerTextInputField();
        }

        [Inject]
        public void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }
        
        public void SetNewQuestionText(string text)
        {
            questionText.text = text;
        }
        
        public void SendAnswerButtonClicked()
        {
            _signalBus.Fire(new SendAnswerButtonClickedSignal());
        }
        
        public void CharacterButtonClicked()
        {
            _signalBus.Fire(new CharacterButtonClickedSignal());
        }
        
        public void ResetAnswerCharacterText()
        {
            Array.ForEach(answerTextCharacters, text => text.text = string.Empty);
        }
        
        public void ResetAnswerText()
        {
            answerText.text = string.Empty;
        }
        
        public void SetScoreText(int score)
        {
            scoreText.text = score.ToString();
        }
        
        public void SetInteractableCharacterButton(bool value)
        {
            characterButton.interactable = value;
        }
        
        public void ActivateAnswerTextInputField()
        {
            answerText.ActivateInputField();
        }
        
        public void SetActiveAnswerCharacterParentObject(int index)
        {
            answerCharacterParentObjects[index].SetActive(true);
        }
    }
}