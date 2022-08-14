using System.Collections.Generic;
using Enemies;
using Factories;
using GameSession;
using UnityEngine;

namespace Spawner
{
    public class AsteroidSpawner : EnemySpawner<Asteroid>, IGameStartListener
    {
        [SerializeField] private int _startAmount;
        [SerializeField] private int _asteroidPartCount;

        private List<Asteroid> _activeAsteroids = new List<Asteroid>();
        
        public void OnStartGame()
        {
            Spawn();
        }

        public void SpawnPartAsteroids(IEnemyPlacer enemyPlacer, EnemyType partAsteroid)
        {
           CreateAsteroids(partAsteroid, enemyPlacer, _asteroidPartCount);
        }

        protected override void SubScribe()
        {
            EventBus.Subscribe<IGameStartListener>(this);
            base.SubScribe();
        }

        protected override void UnSubscribe()
        {
            EventBus.UnSubscribe<IGameStartListener>(this);
            base.UnSubscribe();
        }

        protected override void OnReclaimed(Asteroid asteroid)
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
            CreateAsteroids(EnemyType.LargeAsteroid, EnemyPlacer.CreateAsteroidPlacer(EnemyCollider), _startAmount);
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