using System;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner<T> : MonoBehaviour, IEnemySpawner where T : Enemy
    {
        [SerializeField] private EnemyGenerator<T> _enemyGenerator;

        protected IEnemyPlacer EnemyPlacer;

        private void OnEnable()
        {
            SubScribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        public void Init()
        {
            _enemyGenerator.Init(OnDestroyEnemy);
            EnemyPlacer = GetComponent<EnemyPlacerFactory>().Get();
        }

        public virtual void InitialSpawn()
        {
            
        }

        protected virtual void SubScribe()
        {
            
        }

        protected virtual void UnSubscribe()
        {
            
        }
        
        protected abstract void OnDestroyEnemy(T enemy);

        protected T Create(EnemyType enemyType, IEnemyPlacer enemyPlacer)
        {
            return _enemyGenerator.Create(enemyType, enemyPlacer);
        }

        private void OnGameLaunched()
        {
            Init();
            InitialSpawn();
        }
    }
}