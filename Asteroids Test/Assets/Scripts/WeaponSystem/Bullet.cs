﻿using DefaultNamespace;
using Factories;
using GameSession;
using UnityEngine;

namespace WeaponSystem
{
    public abstract class Bullet : MonoBehaviour
    {
        public IFactoryGameElements RecyclerFactoryGameElements { get; set; }
    
        [SerializeField] private float _unitPerSecond;
    
        private Vector3 _direction;
        private float _maxDistance;
        private float _distanceTraveled;
        private bool _isPaused => PauseManager.GetInstance().IsPaused;

    
        private void Start()
        {
            _maxDistance = ScreenBoundSize.Size.x;
        }
    

        public void Init(Vector2 position, Vector3 direction)
        {
            _direction = direction;
            transform.position = position;
        }
        

        protected abstract bool TryCollisionWith(GameObject gameObject);


        private void Update()
        {
            if(_isPaused) return;
        
            TryMove();
        }

        private void TryMove()
        {
            float deltaMove = _unitPerSecond * Time.deltaTime;
        
            if (_distanceTraveled >= _maxDistance)
            {
                Destroy();
                return;
            }

            transform.position += _direction * deltaMove;
            _distanceTraveled += deltaMove;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (TryCollisionWith(other.gameObject))
            {
                Destroy();
            }
        }

        private void Destroy()
        {
            _distanceTraveled = 0f;
            
            RecyclerFactoryGameElements.Recycle(this);
        }
    }
}
