using System;
using Enemies;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    public enum AsteroidType
    {
        Average,
        Small,
        Large,
    }
    
    [RequireComponent(typeof(AsteroidPool))]
    public class AsteroidFactory : EnemyFactory<Asteroid>
    {
        [SerializeField]
        private EnemyConfig _asteroidLargeConfig, _asteroidAverageConfig, _asteroidSmallConfig;

        private IObjectPool<Asteroid> _asteroidsPool;
        
        private void Awake()
        {
            _asteroidsPool = GetComponent<AsteroidPool>();
        }

        public Asteroid CreateAsteroid(AsteroidType asteroidType, Vector2 position, Vector2 direction)
        {
            EnemyConfig enemyConfig = GetAsteroidConfigByType(asteroidType);
            
            
            Asteroid asteroid = _asteroidsPool.GetFreeElement();
            
            asteroid.Init(asteroidType);
            
            return Create(asteroid, enemyConfig, position, direction);
        }

        public override void Reclaim(Asteroid enemy)
        {
            _asteroidsPool.ReturnToPool(enemy);
        }
        
        private EnemyConfig GetAsteroidConfigByType(AsteroidType asteroidType)
        {
            switch (asteroidType)
            {
                case AsteroidType.Average:
                    return _asteroidAverageConfig;
                case AsteroidType.Small:
                    return _asteroidSmallConfig;
                case AsteroidType.Large:
                    return _asteroidLargeConfig;
                default:
                    throw new Exception("no such config");
            }
        }
    }
}