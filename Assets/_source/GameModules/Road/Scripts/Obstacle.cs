using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Road
{
    public class Obstacle : MonoBehaviour
    {
        [field: SerializeField]
        public ObstacleType ObstacleType { get; private set; } 

        [SerializeField]
        private float _speed;

        [SerializeField]
        private SpriteRenderer _spriteRenderer;

        public void SetSpriteOrder(int order)
        {
            _spriteRenderer.sortingOrder = order;
        }

        public void OnUpdate(float deltaTime)
        {
            transform.Translate(new Vector3(-_speed * deltaTime, 0, 0));
           
        }

        public void Initialize(float speed, int order)
        { 
            _speed = speed;
            SetSpriteOrder(order);
        }
    }


    public enum ObstacleType
    { 
        Snow,
        Stone
    }
}
