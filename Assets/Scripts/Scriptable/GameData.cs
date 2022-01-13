using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "GameData", menuName = "SO/GameData", order = 0)]
    public class GameData : ScriptableObject
    {
        public int lives = 3;
        public int score;
        public GameStateData state;

        public void Init()
        {
            lives = 3;
            score = 0;

            state.onFail = false;
            state.onGame = false;
            state.onPause = false;
            state.onSuccess = false;
        }
    }
}