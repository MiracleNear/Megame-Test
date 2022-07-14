using System;
using Factories;
using UnityEngine;

namespace Enemies
{
    public class Asteroid : Enemy
    {
        public event Action<Asteroid> BulletHit;

        public event Action<Asteroid> Died;

        public AsteroidType AsteroidType { get; private set; }
        
        public void Init(AsteroidType asteroidType)
        {
            AsteroidType = asteroidType;
        }
        
        public override void OnCollisionBullet(Bullet bullet, Action onCollisionSuccessful)
        {
            IsDestroyByPlayer(bullet);

            BulletHit?.Invoke(this);

            onCollisionSuccessful?.Invoke();

            Died?.Invoke(this);
        }


		private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out IAsteroidCollisionHandler handler))
            {
                handler.OnCollisionAsteroid();
                Died?.Invoke(this);
            }
        }

        private void Update()
        {
            Move();
        }
    }
}