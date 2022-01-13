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

        // private void Update()
        // {
        //     if (gameData.lives <= 0 && Input.anyKey)
        //     {
        //         Init();
        //     }
        // }

        private void Init()
        {
            gameData.Init();
            player.OnAnswerSuccess += PlayerSuccess;
            player.Init();
        }

        private void GameOver()
        {
            player.gameObject.SetActive(false);
        }

        private void PlayerSuccess(bool success)
        {
            if (!success) return;

            gameData.state.onSuccess = true;
            SceneManager.LoadSceneAsync("Scenes/Utils/Success");
        }

        public void PlayerDamage()
        {
            player.DeathSequence();

            uiManager.SetLives(gameData.lives - 1);

            if (gameData.lives > 0)
                Invoke(nameof(Init), 3.0f);
            else
                GameOver();
        }
    }
}