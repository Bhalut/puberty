using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "GameData", menuName = "SO/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public int lives = 3;
        public int score;
        public bool isCompleted;
    }
}