using UnityEngine;
using System.Collections;

namespace AISandbox {
    [RequireComponent(typeof(IPlayerActor))]
    public class PlayerController : MonoBehaviour
    {
        private GameObject playerBullet;
        private GameObject bulletPosition;
        //private Transform player;
       // private BulletFire bulletFireScript;
        private float thrust;
     

        private IPlayerActor _actor;
       // private SimplePathFollowingActor simpleActor;
       
        public void Awake()
        {
            thrust = 10.0f;
            _actor = GetComponent<IPlayerActor>();
            //   simpleActor = GameObject.Find("Actor").GetComponent<SimplePathFollowingActor>();
            playerBullet = Resources.Load<GameObject>("Prefabs/BulletPrefab");
            bulletPosition = GameObject.Find("BulletPosition");

            //player = GameObject.Find("Player").GetComponent<Transform>();
          
            //bulletFireScript = GameObject.FindGameObjectWithTag("Bullet").GetComponent<BulletFire>();
          

        }

        private void Start()
        {
          
        }

        private void FixedUpdate()
        {
         


            // Read the inputs.
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))

            {
                float x_axis = Input.GetAxis("Horizontal");
                float y_axis = Input.GetAxis("Vertical");
                _actor.SetPlayerInput(x_axis, y_axis);
            }
            
            // simpleActor.SetVelocity(Vector
            // Pass all parameters to the character control script.

        }

        public void Update()
        {
               if(Input.GetKeyDown("q"))
            {
                //Debug.Log(playerBullet);
               // Debug.Log(bulletFireScript);
                //Debug.Log(player);
                
                GameObject bulletPrefab = Instantiate(playerBullet, transform) as GameObject;
                bulletPrefab.transform.position = bulletPosition.transform.position;
                bulletPrefab.transform.rotation = bulletPosition.transform.rotation;
                bulletPrefab.GetComponent<Rigidbody2D>().AddForce(transform.up * thrust);

                bulletPrefab.GetComponent<Transform>().transform.parent = null;
                //bulletFireScript.rb.AddForce(player.transform.up * thrust);
                //bulletFireScript.transform.parent = null;




            }
        }

         
    }
}