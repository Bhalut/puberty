using Scriptable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameManager : MonoBehaviour
    {
        public Player player;

        public GameData gameData;
        public UIManager uiManager;

        private void Start()
        {
            Init();
            uiManager.Init();
        }

        private void Init()
        {
            player.OnAnswerSuccess += PlayerSuccess;
            player.OnPlayerDamage += PlayerDamage;

            gameData.Init();
            player.Init();
        }

        private void PlayerAgain()
        {
            player.Init();
            gameData.state.onGame = true;
        }

        private void GameOver()
        {
            SceneManager.LoadSceneAsync("Maze");
        }

        private void ChangeScene()
        {
            SceneManager.LoadSceneAsync("Scenes/Utils/Success");
        }

        private void PlayerSuccess(bool success)
        {
            if (!success) return;

            gameData.state.onSuccess = true;
            gameData.state.onGame = false;
            uiManager.SetScore(gameData.score + 1);
            Invoke(nameof(ChangeScene), 1.0f);
        }

        private void PlayerDamage()
        {
            player.DeathSequence();

            uiManager.SetLives(gameData.lives - 1);

            if (gameData.lives > 0)
            {
                gameData.state.onGame = false;
                Invoke(nameof(PlayerAgain), 1.0f);
            }
            else
            {
                Invoke(nameof(GameOver), 1.0f);
            }
        }
    }
}