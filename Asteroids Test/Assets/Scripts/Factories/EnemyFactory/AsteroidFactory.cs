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

        protected override Asteroid GetInstance()
        { 
            Asteroid asteroid = _asteroidsPool.GetFreeElement();
            
            asteroid.Init(_asteroidSpawner);

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

        public override void Reclaim(Asteroid enemy)
        {
            _asteroidsPool.ReturnToPool(enemy);
        }
	}
}