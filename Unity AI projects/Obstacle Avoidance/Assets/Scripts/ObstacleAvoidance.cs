using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace AISandbox
{
    public class ObstacleAvoidance : MonoBehaviour
    {
        public void ResetLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void QuitGame()
        {
            Application.Quit();
        }
    }
}