using System;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner<T> : MonoBehaviour, IEnemySpawner where T : Enemy
    {
        [SerializeField] private EnemyGenerator<T> _enemyGenerator;
        [SerializeField] private GameStarter _gameStarter;
        
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
            _gameStarter.GameLaunched += OnGameLaunched;
        }

        protected virtual void UnSubscribe()
        {
            _gameStarter.GameLaunched -= OnGameLaunched;
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