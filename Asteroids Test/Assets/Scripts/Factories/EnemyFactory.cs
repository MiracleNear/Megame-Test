using Enemies;
using System;
using UnityEngine;

namespace Factories
{
    public enum EnemyType
	{
        SmallAsteroid,
        MediumAsteroid,
        LargeAsteroid,
        Ufo,
	}

    public abstract class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _asteroidLargeConfig, _asteroidMediumConfig, _asteroidSmallConfig, _ufoConfig;

        public Enemy Get(EnemyType enemy, Vector2 position, Vector2 direction)
        {
            EnemyConfig enemyConfig = GetCongigByType(enemy);

            Enemy enemyTemplate = GetInstance();

            enemyTemplate.Init(enemyConfig, enemy);

            return enemyTemplate;
        }

        public abstract void Reclaim(Enemy enemy);

        protected abstract Enemy GetInstance(); 

        private EnemyConfig GetCongigByType(EnemyType enemyType)
		{
            switch(enemyType)
			{
                case EnemyType.SmallAsteroid:
                    return _asteroidSmallConfig;
                case EnemyType.MediumAsteroid:
                    return _asteroidMediumConfig;
                case EnemyType.LargeAsteroid:
                    return _asteroidLargeConfig;
                case EnemyType.Ufo:
                    return _ufoConfig;
                default:
                    throw new Exception("no such type");
			}
		}
    }
}