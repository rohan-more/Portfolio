using UnityEngine;
using System.Collections;
namespace AISandbox
{
    [RequireComponent(typeof(IActor))]
    public class StartingPositionController : MonoBehaviour
    {
        private IActor _actor;
        private Grid _grid;
        private Flocking _flockingScript;
        public GridNode[] positions;

        public bool isStartPositionReached = false;
        private float speed = 12.0f;
        // Use this for initialization
        void Start()
        {

            _grid = GameObject.Find("Grid").GetComponent<Grid>();
            _flockingScript = GameObject.Find("Pathfollowing").GetComponent<Flocking>();
            positions = new GridNode[]{
                _grid.GetNode(19, 28),
                 _grid.GetNode(28, 15) ,
                 _grid.GetNode(23,28)
                    };

        }

        // Update is called once per frame
        void FixedUpdate()
        {

            float step = speed * Time.deltaTime;
            for (int i=0;i<_flockingScript.TOTALACTORS;i++)
            {

               _flockingScript.actors[i].transform.position = Vector3.MoveTowards(_flockingScript.actors[i].transform.position, positions[i].transform.position, step);
            }

        }
    }
}
