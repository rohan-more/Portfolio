using UnityEngine;
using System.Collections;
namespace AISandbox
{


    public class BulletFire : MonoBehaviour {
        float speed;
        public float thrust;
        public Rigidbody2D rb;
        private GameObject player;
        private Renderer _renderer;
        private Flocking flock;
        // Use this for initialization
        private void Start() {
            speed = 2.0f;
            thrust = 0.1f;
            rb = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
            _renderer = GetComponent<Renderer>();
            flock = GameObject.Find("Pathfollowing").GetComponent<Flocking>();
        }

        // Update is called once per frame
        void Update() {



            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));



        }

        void OnBecameInvisible()
        {
            Destroy(this.gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Enemy")
            {
                other.gameObject.SetActive(false);
                Destroy(this.gameObject);
                //  flock.actors.le = -1;
                flock.Total -= 1;
            }
        }
    }
    
}
