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

    public abstract class EnemyFactory<T> : MonoBehaviour where T : Enemy
    {
        public T Get(EnemyType enemy)
        {
            EnemyConfig enemyConfig = GetConfigByType(enemy);

            T enemyTemplate = GetInstance();
            
            enemyTemplate.Init(enemyConfig, enemy);
            
            return enemyTemplate;
        }

        public abstract void Reclaim(T enemy);

        protected abstract T GetInstance();

        protected abstract EnemyConfig GetConfigByType(EnemyType enemyType);
    }
}