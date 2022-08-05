using System.Collections.Generic;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    [RequireComponent(typeof(AsteroidPlacerFactory))]
    public class AsteroidSpawner : EnemySpawner<Asteroid>
    {
        [SerializeField] private int _startAmount;
        
        private List<Asteroid> _activeAsteroids = new List<Asteroid>();

        public override void InitialSpawn()
        {
            Spawn();
        }

        public void SpawnPartAsteroids(EnemyType asteroidPartType, IEnemyPlacer asteroidPartPlacer, int count)
        {
            CreateAsteroids(asteroidPartType, asteroidPartPlacer, count);
        }

        protected override void OnDestroyEnemy(Asteroid asteroid)
        {
            _activeAsteroids.Remove(asteroid);

            if (_activeAsteroids.Count == 0)
            {
                _startAmount++;
                
                Spawn();
            }
        }

        private void Spawn()
        {
            CreateAsteroids(EnemyType.LargeAsteroid, EnemyPlacer, _startAmount);
        }

        private void CreateAsteroids(EnemyType asteroidType, IEnemyPlacer enemyPlacer, int count)
        {
            for (int i = 0; i < count; i++)
            {
                Asteroid asteroid = Create(asteroidType, enemyPlacer);
                
                _activeAsteroids.Add(asteroid);
            }
        }
    }
}