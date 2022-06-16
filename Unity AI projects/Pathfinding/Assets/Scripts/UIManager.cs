using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace AISandbox
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Button startNodeButton;
        [SerializeField] private Button endNodeButton;
        [SerializeField] private Button blockedNodeButton;
        [SerializeField] private Button resetButton;
        [SerializeField] private const float _node_width = 1.92f;
        [SerializeField] private const float _node_height = 1.92f;
        private bool isOpen;
        public bool is_Open
        {
            get
            {
                return isOpen;
            }
            set
            {
                isOpen = value;
            }
        }
        void Start()
        {
            startNodeButton.onClick.AddListener(OnClickStart);
            endNodeButton.onClick.AddListener(OnClickEnd);
            blockedNodeButton.onClick.AddListener(OnClickBlock);
            resetButton.onClick.AddListener(OnCLickReset);
        }

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 world_pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 local_pos = transform.InverseTransformPoint(world_pos);
                // This trick makes a lot of assumptions that the nodes haven't been modified since initialization.
                int column = Mathf.FloorToInt(local_pos.x / _node_width);
                int row = Mathf.FloorToInt(-local_pos.y /_node_height);
                if (row >= 0 && row < grid._nodes.GetLength(0)
                 && column >= 0 && column < grid._nodes.GetLength(1))
                {
                    GridNode node = grid._nodes[row, column];
                    if (Input.GetMouseButtonDown(0) && grid._draw_blocked == true)
                    {
                        grid._draw_blocked = !node.blocked;
                    }

                    if (node.blocked != grid._draw_blocked)
                    {
                        node.blocked = grid._draw_blocked;
                    }
                    if (Input.GetMouseButtonDown(0) && grid.draw_start == true)
                    {
                        grid.draw_start = !node.startnode;
                    }
                    if (node.startnode != grid.draw_start)
                    {
                        node.startnode = grid.draw_start;
                    }
                    if (Input.GetMouseButtonDown(0) && grid.draw_end == true)
                    {
                        grid.draw_end = !node.endnode;
                    }
                    if (node.endnode != grid.draw_end)
                    {
                        node.endnode = grid.draw_end;
                    }
                }
            }
        }



        public void OnClickStart()
        {

            grid.draw_start = true;
            grid._draw_blocked = false;
            grid.draw_end = false;
            isOpen = false;
        }

        public void OnClickEnd()
        {
            grid.draw_end = true;
            grid._draw_blocked = false;
            grid.draw_start = false;
            isOpen = false;
        }

        public void OnClickBlock()
        {
            grid._draw_blocked = true;
            grid.draw_start = false;
            grid.draw_end = false;
            isOpen = false;
        }

        public void OnCLickReset()
        {
            SceneManager.LoadScene("Pathfinding");
        }
    }


}

