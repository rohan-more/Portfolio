using UnityEngine;
using System.Collections;

namespace AISandbox
{
    public class Buttons : MonoBehaviour
    {
        private ObstacleAvoidance obstacleAvoidanceScript;
        void Start()
        {
            obstacleAvoidanceScript = GameObject.Find("Obstacle Avoidance").GetComponent<ObstacleAvoidance>();
        }
        public void ResetLevel()
        {
            obstacleAvoidanceScript.ResetLevel();
        }
        public void QuitGame()
        {
            obstacleAvoidanceScript.QuitGame();
        }
    }
}
