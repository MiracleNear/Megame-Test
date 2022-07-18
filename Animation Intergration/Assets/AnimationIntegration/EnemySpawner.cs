using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimationIntegration
{
    public class EnemySpawner : MonoBehaviour
    {
        public Transform StartingSpawnPoint;
        public Player Player;
        public Enemy Enemy;
        public float RadiusSpawn;
        
        
        private void Start()
        {
            StartSpawn();
        }

        private void StartSpawn()
        {
            CreateEnemy(StartingSpawnPoint.position);
        }

        private void CreateEnemy(Vector3 position)
        {
            Enemy enemy = Instantiate(Enemy, position, Quaternion.identity);

            Player.Enemy = enemy;
            enemy.Died += OnDiedEnemy;
        }

        private void OnDiedEnemy()
        {
            Vector2 position = Random.insideUnitCircle.normalized * RadiusSpawn;
            
            CreateEnemy(new Vector3(position.x, 0f, position.y));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(Vector3.zero, RadiusSpawn);
        }
    }
}