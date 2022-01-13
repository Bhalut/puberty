using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Controller
{
    public class SuccessController : MonoBehaviour
    {
        [SerializeField] private Button playAgainButton;

        private void Start()
        {
            playAgainButton.onClick.AddListener(PlayAgain);
        }

        private static void PlayAgain()
        {
            SceneManager.LoadSceneAsync("Maze");
        }
    }
}
