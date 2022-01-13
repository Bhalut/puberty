using System.Collections.Generic;
using Q_A;
using Scriptable;
using Scriptable.Q_A;
using TMPro;
using UnityEngine;
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
        public Answer[] answers = new Answer[4];

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
            SetQuestion();
            SetAnswer(answers);

            uiBeginContainer.SetActive(false);
            gameData.state.onGame = true;
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
            questionText.enabled = true;
            questionText.text = question.title;
        }

        private void SetAnswer(IReadOnlyList<Answer> answerList)
        {
            for (var i = 0; i < answerList.Count; i++)
            {
                answerList[i].data = question.answers[i];
                answerList[i].GetComponent<SpriteRenderer>().enabled = true;
                answerList[i].GetComponentInChildren<TMP_Text>().text = answerList[i].data.title;
            }
        }

        private void GameOver(bool lose)
        {
            gameOverText.enabled = lose;
        }
    }
}