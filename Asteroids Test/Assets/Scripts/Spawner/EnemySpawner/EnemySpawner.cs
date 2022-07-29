using System;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public abstract class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyPublisher _enemyPublisher;
        [SerializeField] private EnemyFactory _enemyFactory;

		private void Awake()
		{
            InitSpawner();
		}

		public Enemy Spawn(EnemyType enemyType)
		{
            Vector2 position = GetPosition();
            Vector2 direction = GetDirectionFrom(position);

            Enemy enemy = _enemyFactory.Get(enemyType, position, direction);

            enemy.Died += OnDied;

            _enemyPublisher.Publish(enemy);

            return enemy;
		}

        protected virtual void InitSpawner()
        {

        }

        protected abstract Vector2 GetPosition();

        protected abstract Vector2 GetDirectionFrom(Vector2 position);


        private void OnDied(Enemy enemy)
        {
            enemy.Died -= OnDied;
            _enemyPublisher.Remove(enemy);
            _enemyFactory.Reclaim(enemy);
        }
    }
}