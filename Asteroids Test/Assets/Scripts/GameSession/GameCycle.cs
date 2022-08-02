using System.Collections.Generic;
using Enemies;
using Factories;
using UnityEngine;

public class GameCycle : MonoBehaviour
{
    [SerializeField] private EnemyGenerator<Asteroid> _asteroidGenerator;
    [SerializeField] private EnemyGenerator<Ufo> _ufoGenerator;
    [SerializeField] private int _startingNumberOfAsteroids;

    private List<Asteroid> _activeAsteroids = new List<Asteroid>();
    
    private void Awake()
    {
        _asteroidGenerator.Init(OnSpawned, OnRemoved);
        _ufoGenerator.Init(OnSpawned, OnRemoved);
    }

    private void Start()
    {
        _asteroidGenerator.Generate(EnemyType.LargeAsteroid, _startingNumberOfAsteroids);
        _ufoGenerator.Generate(EnemyType.Ufo, 1);
    }

    private void OnSpawned(Asteroid asteroid)
    {
        _activeAsteroids.Add(asteroid);
    }

    private void OnRemoved(Asteroid asteroid)
    {
        _activeAsteroids.Remove(asteroid);

        if (_activeAsteroids.Count == 0)
        {
            _startingNumberOfAsteroids += 1;
            
            _asteroidGenerator.Generate(EnemyType.LargeAsteroid, _startingNumberOfAsteroids);
        }
    }

    private void OnSpawned(Ufo ufo)
    {
        
    }

    private void OnRemoved(Ufo ufo)
    {
        
    }
}
