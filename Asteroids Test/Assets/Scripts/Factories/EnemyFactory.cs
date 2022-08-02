using Enemies;
using System;
using UnityEngine;

namespace Factories
{
    public enum EnemyType
	{
        LargeAsteroid,
        MediumAsteroid,
        SmallAsteroid,
        Ufo,
	}

    public abstract class EnemyFactory : MonoBehaviour
    {
        public Enemy Get(EnemyType enemy)
        {
            EnemyConfig enemyConfig = GetConfigByType(enemy);

            Enemy enemyTemplate = GetInstance();

            enemyTemplate.Init(enemyConfig, enemy);

            return enemyTemplate;
        }

        public abstract void Reclaim(Enemy enemy);

        protected abstract Enemy GetInstance();

        protected abstract EnemyConfig GetConfigByType(EnemyType enemyType);
    }
}