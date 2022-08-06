using System;
using CollisionInterface;
using Factories;
using Spawner;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(AsteroidPartPlacerFactory))]
    public class Asteroid : Enemy, IUfoBulletCollisionHandler
    {
        private bool _isInit;

        private AsteroidSpawner _asteroidSpawner;
        private IEnemyPlacer _asteroidPartPlacer;

        public void Init(AsteroidSpawner asteroidSpawner)
        {
            if (!_isInit)
            {
                _asteroidSpawner = asteroidSpawner;
                _asteroidPartPlacer = GetComponent<AsteroidPartPlacerFactory>().Get();
            }
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
            if (Type == EnemyType.LargeAsteroid)
            {
                _asteroidSpawner.SpawnPartAsteroids(EnemyType.MediumAsteroid, _asteroidPartPlacer, 2);
            }
            else if (Type == EnemyType.MediumAsteroid)
            {
                _asteroidSpawner.SpawnPartAsteroids(EnemyType.SmallAsteroid, _asteroidPartPlacer, 2);
            }
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
            }
        }
    }
}