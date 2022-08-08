using System;
using Enemies;
using Factories;
using GameSession;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner<T> : MonoBehaviour,  IGameStartListener, IEnemySpawner where T : Enemy
    {
        [SerializeField] private EnemyGenerator<T> _enemyGenerator;

        protected IEnemyPlacer EnemyPlacer;

        private void Awake()
        {
            EventBus.Subscribe<IGameStartListener>(this);
        }

        private void OnEnable()
        {
            SubScribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }

        private void OnDestroy()
        {
            EventBus.UnSubscribe<IGameStartListener>(this);
        }

        public void Init()
        {
            _enemyGenerator.Init(OnDestroyEnemy);
            EnemyPlacer = GetComponent<EnemyPlacerFactory>().Get();
        }

        public void OnStartGame()
        {
            Init();
            InitialSpawn();
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
        
    }
}