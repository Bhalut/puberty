using UnityEngine;

namespace Scriptable
{
    [CreateAssetMenu(fileName = "GameStateData", menuName = "SO/GameStateData", order = 1)]
    public class GameStateData : ScriptableObject
    {
        public bool onGame;
        public bool onPause;
        public bool onSuccess;
        public bool onFail;
    }
}