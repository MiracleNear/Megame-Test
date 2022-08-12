using System;
using CollisionInterface;
using Factories;
using Spawner;
using UnityEngine;

namespace Enemies
{
    public class Asteroid : Enemy, IUfoBulletCollisionHandler
    {
        private AsteroidSpawner _asteroidSpawner;
        private AsteroidType _asteroidType;
        
        public void Init(AsteroidSpawner asteroidSpawner, AsteroidType asteroidType)
        {
            _asteroidSpawner = asteroidSpawner;
            _asteroidType = asteroidType;
        }

        public bool TryBreakAsteroid(out EnemyType nextPartAsteroid)
        {
            nextPartAsteroid = default;

            if (!_asteroidType.IsPrimitive)
            {
                nextPartAsteroid = _asteroidType.NextPartAsteroid;
                return true;
            }

            return false;
        }
        
        public override void OnCollisionPlayerBullet(Action<int> action)
        {
            CreatePartsAsteroid();
            
            base.OnCollisionPlayerBullet(action);
        }

        public void OnCollisionUfoBullet()
        {
            CreatePartsAsteroid();
            
            DestroySelf();
        }

        private void CreatePartsAsteroid()
        {
            _asteroidSpawner.SpawnPartAsteroidsFor(this);
        }
        
        private void OnCollisionEnter2D(Collision2D other)
        { 
            if (other.gameObject.TryGetComponent(out IAsteroidCollisionHandler handler))
            {
                handler.OnCollisionAsteroid();
                DestroySelf();
            }
        }
    }
}