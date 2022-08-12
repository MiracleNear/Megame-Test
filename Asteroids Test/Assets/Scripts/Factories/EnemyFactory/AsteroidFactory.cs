using System;
using Enemies;
using ObjectPools;
using Spawner;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(AsteroidPool))]
    public class AsteroidFactory : EnemyFactory<Asteroid>
    {
        [SerializeField] private EnemyConfig _asteroidLargeConfig, _asteroidMediumConfig, _asteroidSmallConfig;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;

        private IObjectPool<Asteroid> _asteroidsPool;
        private void Awake()
        {
            _asteroidsPool = GetComponent<AsteroidPool>();
        }

        protected override void Reclaim(Asteroid element)
        {
            base.Reclaim(element);
            
            _asteroidsPool.ReturnToPool(element);
        }
        

        protected override Asteroid GetInstance(EnemyType enemyType)
        { 
            Asteroid asteroid = _asteroidsPool.GetFreeElement();
            
            asteroid.Init(_asteroidSpawner, AsteroidType.ConvertEnemyTypeToAsteroidType(enemyType));

            return asteroid;
        }

        protected override EnemyConfig GetConfigByType(EnemyType enemyType)
        {
            switch(enemyType)
            {
                case EnemyType.SmallAsteroid:
                    return _asteroidSmallConfig;
                case EnemyType.MediumAsteroid:
                    return _asteroidMediumConfig;
                case EnemyType.LargeAsteroid:
                    return _asteroidLargeConfig;
                default:
                    throw new Exception("no such type");
            }
        }
    }
}