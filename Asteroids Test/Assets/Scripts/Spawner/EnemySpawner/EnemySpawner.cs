using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner<T> : MonoBehaviour where T : Enemy
    {
        [SerializeField] private EnemyFactory<T> _enemyFactory;
        
        private void OnEnable()
        {
            SubScribe();
        }

        private void OnDisable()
        {
            UnSubscribe();
        }
        
        
        protected virtual void SubScribe()
        {
            _enemyFactory.Reclaimed += OnReclaimed;
        }
        
        protected virtual void UnSubscribe()
        {
            _enemyFactory.Reclaimed -= OnReclaimed;
        }
        
        protected abstract void OnReclaimed(T enemy);

        protected T Create(EnemyType enemyType, IEnemyPlacer enemyPlacer)
        {
            T enemy = _enemyFactory.Get(enemyType, enemyPlacer.GetPosition(), enemyPlacer.GetDirectionFrom(enemyPlacer.GetPosition()));
            
            return enemy;
        }
    }
}