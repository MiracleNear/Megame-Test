using Enemies;
using UnityEngine;

namespace Factories
{
    public abstract class EnemyFactory<T> : MonoBehaviour where T : Enemy
    {
        public abstract void Reclaim(T enemy);

        protected T Create(T enemy, EnemyConfig config, Vector2 position, Vector2 direction)
        {
            enemy.Init(config, position, direction);
            
            return enemy;
        }
    }
}