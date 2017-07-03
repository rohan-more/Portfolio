using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace AISandbox
{
    public class Buttons : MonoBehaviour
    {
        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            GameManager.gameManager.ResetGameManager();
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}
