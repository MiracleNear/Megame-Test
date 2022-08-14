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
        [SerializeField] private AsteroidConfig _asteroidLargeConfig, _asteroidMediumConfig, _asteroidSmallConfig;
        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private float _yawAngle;

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

            AsteroidConfig asteroidConfig = GetConfigByType(enemyType);
            
            asteroid.Init(asteroidConfig,AsteroidType.CreateAsteroidType(_asteroidSpawner, _yawAngle, enemyType));

            return asteroid;
        }

        private AsteroidConfig GetConfigByType(EnemyType enemyType)
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