using System;
using Spawner;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(AsteroidPartSpawner))]
    public class Asteroid : Enemy
    {
        public event Action<Asteroid> BulletHit;

        public AsteroidPartSpawner AsteroidPartSpawner { get; private set; }
        
        private void Awake()
        {
            AsteroidPartSpawner = GetComponent<AsteroidPartSpawner>();
        }

        private void Update()
        {
            Move();
        }

        private void OnCollisionEnter2D(Collision2D other)
        { 
            if (other.gameObject.TryGetComponent(out IAsteroidCollisionHandler handler))
            {
                handler.OnCollisionAsteroid();
                DestroySelf();
                PlaySoundDeath();
            }
        }
        
        protected override void OnDestroyByPlayer()
        {
            BulletHit?.Invoke(this);
        }
    }
}