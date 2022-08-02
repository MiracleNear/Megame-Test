using System;
using Enemies;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(AsteroidPool))]
    public class AsteroidFactory : EnemyFactory
    {
        [SerializeField] private EnemyConfig _asteroidLargeConfig, _asteroidMediumConfig, _asteroidSmallConfig;
        
        private IObjectPool<Asteroid> _asteroidsPool;
        private void Awake()
        {
            _asteroidsPool = GetComponent<AsteroidPool>();
        }

        protected override Enemy GetInstance()
        {
            return _asteroidsPool.GetFreeElement();
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

        public override void Reclaim(Enemy enemy)
        {
            _asteroidsPool.ReturnToPool(enemy as Asteroid);
        }
	}
}