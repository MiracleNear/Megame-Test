using System;
using Factories;
using UnityEngine;

namespace Enemies
{
    public class Asteroid : Enemy
    {
        public event Action<Asteroid> BulletHit;
        public AsteroidType AsteroidType { get; private set; }
        
        public void Init(AsteroidType asteroidType)
        {
            AsteroidType = asteroidType;
        }
        
        public override void OnCollisionBullet()
        {
            BulletHit?.Invoke(this);
            
            base.OnCollisionBullet();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IAsteroidCollisionHandler handler))
            {
                handler.OnCollisionAsteroid();
                DestroySelf();
            }
        }

        private void Update()
        {
            Move();
        }
    }
}