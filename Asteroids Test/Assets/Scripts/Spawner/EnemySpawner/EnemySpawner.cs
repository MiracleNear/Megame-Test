using Enemies;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        private void Awake()
        {
            InitSpawner();
        }

        public void Spawn(Enemy enemy)
        {
            Vector2 position = GetPosition();
            Vector2 direction = GetDirectionFrom(position);
            
            enemy.Direction = direction;
            enemy.transform.position = position;
        }

        protected abstract void InitSpawner();

        protected abstract Vector2 GetPosition();

        protected abstract Vector2 GetDirectionFrom(Vector2 position);
        
    }
}