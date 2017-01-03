using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace AISandbox
{
    public class PursuitAndEvasion : MonoBehaviour
    {
        private const float SPAWN_RANGE = 10.0f;

        public SimpleActor _target_actor;
        public SimpleActor _pursuing_actor;
        public SimpleActor _evading_actor;

        private void Start()
        {
            // Choose a random position for the target actor
            Vector3 position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
            _target_actor.transform.position = position;

            // The pursuing and evading actor start at the same position
            position = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
            _pursuing_actor.transform.position = position;
            _evading_actor.transform.position = position;

            // Reset the level every 15 seconds
            Invoke("ResetLevel", 15.0f);
        }

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