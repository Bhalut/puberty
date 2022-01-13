using UnityEngine;

namespace Scriptable.Q_A
{
    [CreateAssetMenu(fileName = "Answer", menuName = "SO/Q&A/AnswerData", order = 1)]
    public class AnswerData : ScriptableObject
    {
        [TextArea] public string title;

        public bool isCorrect;
    }
}