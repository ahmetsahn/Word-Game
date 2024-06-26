using System.Collections.Generic;
using Runtime.QuestionSystem.Model.ScriptableObject;
using UnityEngine;

namespace Runtime.QuestionSystem.Model
{
    public class QuestionModel
    {
        private readonly List<QuestionSo> _fourCharacterQuestionsSo;
        private readonly List<QuestionSo> _fiveCharacterQuestionsSo;
        private readonly List<QuestionSo> _sixCharacterQuestionsSo;
        private readonly List<QuestionSo> _sevenCharacterQuestionsSo;
        private readonly List<QuestionSo> _eightCharacterQuestionsSo;
        private readonly List<QuestionSo> _nineCharacterQuestionsSo;
        private readonly List<QuestionSo> _tenCharacterQuestionsSo;

        public readonly List<QuestionSo> SelectedQuestions;
        
        public QuestionSo CurrentQuestionSo;
        
        public int SendAnswerButtonPressCount = 0;
        public int CharacterButtonPressCount = 0;
        public int QuestionIndex = 0;
        public int Score = 0;
        public int AnswerCharacterParentIndex = 0;

        public QuestionModel(
            List<QuestionSo> fourCharacterQuestionsSo,
            List<QuestionSo> fiveCharacterQuestionsSo,
            List<QuestionSo> sixCharacterQuestionsSo,
            List<QuestionSo> sevenCharacterQuestionsSo,
            List<QuestionSo> eightCharacterQuestionsSo,
            List<QuestionSo> nineCharacterQuestionsSo,
            List<QuestionSo> tenCharacterQuestionsSo)
        {
            _fourCharacterQuestionsSo = fourCharacterQuestionsSo;
            _fiveCharacterQuestionsSo = fiveCharacterQuestionsSo;
            _sixCharacterQuestionsSo = sixCharacterQuestionsSo;
            _sevenCharacterQuestionsSo = sevenCharacterQuestionsSo;
            _eightCharacterQuestionsSo = eightCharacterQuestionsSo;
            _nineCharacterQuestionsSo = nineCharacterQuestionsSo;
            _tenCharacterQuestionsSo = tenCharacterQuestionsSo;
            
            SelectedQuestions = new List<QuestionSo>
            {
                GetRandomFourCharacterQuestion(),
                GetRandomFourCharacterQuestion(),
                GetRandomFiveCharacterQuestion(),
                GetRandomFiveCharacterQuestion(),
                GetRandomSixCharacterQuestion(),
                GetRandomSixCharacterQuestion(),
                GetRandomSevenCharacterQuestion(),
                GetRandomSevenCharacterQuestion(),
                GetRandomEightCharacterQuestion(),
                GetRandomEightCharacterQuestion(),
                GetRandomNineCharacterQuestion(),
                GetRandomNineCharacterQuestion(),
                GetRandomTenCharacterQuestion(),
                GetRandomTenCharacterQuestion()
            };
        }
        
        private QuestionSo GetRandomFourCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _fourCharacterQuestionsSo.Count);
            var question = _fourCharacterQuestionsSo[questionIndex];
            _fourCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        private QuestionSo GetRandomFiveCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _fiveCharacterQuestionsSo.Count);
            var question = _fiveCharacterQuestionsSo[questionIndex];
            _fiveCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        private QuestionSo GetRandomSixCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _sixCharacterQuestionsSo.Count);
            var question = _sixCharacterQuestionsSo[questionIndex];
            _sixCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        private QuestionSo GetRandomSevenCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _sevenCharacterQuestionsSo.Count);
            var question = _sevenCharacterQuestionsSo[questionIndex];
            _sevenCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        private QuestionSo GetRandomEightCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _eightCharacterQuestionsSo.Count);
            var question = _eightCharacterQuestionsSo[questionIndex];
            _eightCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        private QuestionSo GetRandomNineCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _nineCharacterQuestionsSo.Count);
            var question = _nineCharacterQuestionsSo[questionIndex];
            _nineCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }

        private QuestionSo GetRandomTenCharacterQuestion()
        {
            var questionIndex = Random.Range(0, _tenCharacterQuestionsSo.Count);
            var question = _tenCharacterQuestionsSo[questionIndex];
            _tenCharacterQuestionsSo.RemoveAt(questionIndex);
            return question;
        }
        
        public void ReduceScore(int amount)
        {
            Score -= amount;
        }
        
        public void IncreaseScore(int amount)
        {
            Score += amount;
        }
        
        public void IncreaseSendAnswerButtonPressCount()
        {
            SendAnswerButtonPressCount++;
        }
        
        public void IncreaseQuestionIndex()
        {
            QuestionIndex++;
        }

        public void SetNewQuestion()
        {
            CurrentQuestionSo = SelectedQuestions[QuestionIndex];
        }

        public void ResetCharacterButtonPressCount()
        {
            CharacterButtonPressCount = 0;
        }
        
        public void IncreaseAnswerCharacterParentIndex()
        {
            AnswerCharacterParentIndex++;
        }
    }
}
