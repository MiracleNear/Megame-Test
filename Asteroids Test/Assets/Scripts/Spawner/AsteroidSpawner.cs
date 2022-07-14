using System;
using System.Collections.Generic;
using DefaultNamespace;
using Enemies;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class AsteroidSpawner : EnemySpawner
    {
        [SerializeField] private AsteroidFactory _asteroidFactory;

        private Transform _spawnCenter;
        private int _numberOfAsteroids = 2;
        private float _offsetFromScreenBorder = 1f;
        private List<Asteroid> _activeAsteroids;
        private Vector2 _halfSizeScreen => ScreenBoundSize.HalfSize;

        private void Awake()
        {
            _activeAsteroids = new List<Asteroid>();
        }

        public override void Init(Transform spawnCenter)
		{
            _spawnCenter = spawnCenter;

            if(_activeAsteroids.Count == 0)
			{
                SpawnAsteroids();
			}
		}

        public override void Dispose()
        {
            _spawnCenter = null;
        }

        private void Remove(Asteroid asteroid)
        {
            _activeAsteroids.Remove(asteroid);
            
            if (_activeAsteroids.Count == 0)
            {
                _numberOfAsteroids += 1;
                
                SpawnAsteroids();
            }

            _asteroidFactory.Reclaim(asteroid);
        }
        
        private void SpawnAsteroids()
        {
            if(_spawnCenter == null) return;

            for (int i = 0; i < _numberOfAsteroids; i++)
            {
                Vector2 randomDirection = GerRandomDirectionForAsteroid();
                Vector2 randomPosition = GetRandomPointWithinThePlayArea();
                
                SpawnAsteroid(AsteroidType.Large, randomPosition, randomDirection);
            }
        }
        
        private Vector2 GetRandomPointWithinThePlayArea()
        {
            float randomPositionX = Random.Range(-_halfSizeScreen.x + _offsetFromScreenBorder, _halfSizeScreen.x - _offsetFromScreenBorder);
            float randomPositionY = Random.Range(-_halfSizeScreen.y + _offsetFromScreenBorder, _halfSizeScreen.y - _offsetFromScreenBorder);

            return new Vector2(randomPositionX, randomPositionY);
        }
        
        private Vector2 GerRandomDirectionForAsteroid()
        {
            return Random.rotation * Vector2.up;
        }

        private void OnBulletHit(Asteroid asteroid)
        {
            if (asteroid.AsteroidType == AsteroidType.Large)
            {
                CreateAsteroidParts(asteroid, AsteroidType.Average);
            }
            else if (asteroid.AsteroidType == AsteroidType.Average)
            {
                CreateAsteroidParts(asteroid, AsteroidType.Small);
            }
        }

        private void CreateAsteroidParts(Asteroid parent, AsteroidType type)
        {
            Quaternion rotation = Quaternion.AngleAxis(-45f, Vector3.forward);

            for (int i = 0; i < 2; i++)
            {
                Vector3 directionPartAsteroid = rotation * parent.Direction;

                SpawnAsteroid(type, parent.transform.position, directionPartAsteroid);
                
                rotation = Quaternion.Inverse(rotation);
            }
        }

        private void SpawnAsteroid(AsteroidType type, Vector2 position, Vector2 direction)
        {
            Asteroid asteroid = _asteroidFactory.CreateAsteroid(type, position, direction);

            asteroid.BulletHit += OnBulletHit;

            asteroid.Died += Remove;

            _activeAsteroids.Add(asteroid);

            Add(asteroid);
        }
	}
}