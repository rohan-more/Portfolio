using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace AISandbox
{
    public class PursuitAndEvasion : MonoBehaviour
    {
        private const float SPAWN_RANGE = 10.0f;
        [SerializeField]
        private Transform _target_actor;
        [SerializeField]
        private Transform _pursuing_actor;
        [SerializeField]
        private Transform _evading_actor;
        [SerializeField]
        private SimpleActor _target;
        [SerializeField]
        private SimpleActor _pursuing;
        [SerializeField]
        private SimpleActor _evading;
        private Vector3 spawnPosition;

        private void Start()
        {
            // Choose a random position for the target actor
            ResetActors();
        }

        public void ResetActors()
        {
            _target_actor.position = Vector3.zero; 
            _pursuing_actor.position = Vector3.zero; 
            _evading_actor.position= Vector3.zero;
            _target.ResetVectors();
            _pursuing.ResetVectors();
            _evading.ResetVectors();
            spawnPosition = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
            _target_actor.position = spawnPosition;

            // The pursuing and evading actor start at the same position
            spawnPosition = new Vector3(Random.Range(-SPAWN_RANGE, SPAWN_RANGE), Random.Range(-SPAWN_RANGE, SPAWN_RANGE), 0.0f);
            _pursuing_actor.position = spawnPosition;
            _evading_actor.position = spawnPosition;

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