using UnityEngine;

namespace Runtime.QuestionSystem.Model.ScriptableObject
{
    [CreateAssetMenu(menuName = "Scriptable Object/QuestionData", fileName = "Question", order = 0)]
    public class QuestionSo : UnityEngine.ScriptableObject
    {
        [TextArea(3, 10)]
        public string questionText;
        
        [TextArea(3, 10)]
        public string answer;
        
        public int scoreValue;
    }
}