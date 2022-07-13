using System;
using Enemies;
using UnityEngine;

namespace Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        public event Action<Enemy> Spawned;

        [SerializeField] private AsteroidSpawner _asteroidSpawner;
        [SerializeField] private UfoSpawner _ufoSpawner;
        
        public void Register(Enemy enemy)
        {
            enemy.Destroyed += UnRegister;
            Spawned?.Invoke(enemy);
        }

        public void UnRegister(Enemy enemy)
        {
            enemy.Destroyed -= UnRegister;
            
            if (enemy is Asteroid asteroid)
            {
                _asteroidSpawner.Remove(asteroid);
            }
            else if(enemy is Ufo ufo)
            {
                _ufoSpawner.Remove(ufo);
            }
        }
    }
}