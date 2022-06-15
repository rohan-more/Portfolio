using UnityEngine;
using System.Collections;
using System;

namespace AISandbox
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class CircleObstacle : MonoBehaviour
    {
        private readonly Color COLLIDE_COLOR = Color.red;
        private SpriteRenderer _renderer;
        private Color _orig_color;
        private float _radius;
        [SerializeField]  private Sprite _circle_selected;
        private Sprite _orig_circle;
        private SimpleActor[] actors;
        private bool collision = false;
        private void Start()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _orig_color = _renderer.color;           
            _radius = _renderer.bounds.extents.x;
            _orig_circle = _renderer.sprite;
            actors = FindObjectsOfType<SimpleActor>();
        }

        private void FixedUpdate()
        {
            collision = false;
            CheckCollision();
            _renderer.color = collision ? COLLIDE_COLOR : _orig_color;
        }

        /// <summary>
        /// Check collision with each actor
        /// </summary>
        private void CheckCollision()
        {
            foreach (SimpleActor actor in actors)
            {
                float sqr_dist = (actor.transform.position - transform.position).sqrMagnitude;
                float sqr_min_dist = Mathf.Pow(_radius + actor.radius, 2);
                if (sqr_min_dist > sqr_dist)
                {
                    collision = true;
                    break;
                }
            }
        }

        /// <summary>
        /// change obstacle sprite
        /// </summary>
        public void SelectedCircle()
        {
            _renderer.sprite = _circle_selected;
        }
        public void OriginalCircle()
        {
            _renderer.sprite = _orig_circle;
        }
    }
}
