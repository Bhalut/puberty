using UnityEngine;

namespace Scriptable.Q_A
{
    [CreateAssetMenu(fileName = "Question", menuName = "SO/Q&A/QuestionData", order = 0)]
    public class QuestionData : ScriptableObject
    {
        [TextArea] public string title;

        public AnswerData[] answers = new AnswerData[4];
    }
}