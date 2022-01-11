using TMPro;
using Scriptable;
using UnityEngine;
using Scriptable.Q_A;
using UnityEngine.UI;

namespace Core
{
    public class UIManager : MonoBehaviour
    {
        [Header("HUD")]
        public TMP_Text scoreText;
        public TMP_Text livesText;
        public TMP_Text gameOverText;

        [Header("UI Begin")] 
        public GameObject uiBeginContainer;
        public Button playButton;
        
        [Header("UI Game")]
        public TMP_Text questionText;

        [Header("Data")]
        public GameData gameData;
        public QuestionData question;

        public void Init()
        {
            SetScore(0);
            SetLives(0);
            GameOver(false);

            playButton.onClick.AddListener(StartGame);
        }

        private void StartGame()
        {
            uiBeginContainer.SetActive(false);
        }

        public void SetLives(int amount)
        {
            gameData.lives = amount;
            livesText.text = $"LIVES: {amount.ToString()}";
        }

        public void SetScore(int amount)
        {
            gameData.score = amount;
            scoreText.text = $"SCORE: {amount.ToString().PadLeft(2, '0')}";
        }

        private void SetQuestion()
        {
            questionText.text = question.title;
        }

        private void GameOver(bool lose)
        {
            gameOverText.enabled = lose;
        }
    }
}