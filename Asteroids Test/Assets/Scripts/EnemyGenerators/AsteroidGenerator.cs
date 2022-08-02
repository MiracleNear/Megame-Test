using System;
using Enemies;
using Factories;
using Spawner;
using UnityEngine;

[RequireComponent(typeof(AsteroidSpawner))]
public class AsteroidGenerator : EnemyGenerator<Asteroid>
{
    public override void Init(Action<Asteroid> generated, Action<Asteroid> removed)
    {
        EnemySpawner = GetComponent<AsteroidSpawner>();
        
        base.Init(generated, removed);
    }

    public override void Generate(EnemyType enemyType, int count)
    {
       GenerateWithSpawnStrategy(enemyType, count, EnemySpawner);
    }

    protected override void Removed(Asteroid asteroid)
    {
        asteroid.BulletHit -= OnBulletHit;
        
        base.Removed(asteroid);
    }

    private void GenerateWithSpawnStrategy(EnemyType enemyType, int count, EnemySpawner enemySpawner)
    {
        for (int i = 0; i < count; i++)
        {
            Asteroid asteroid = GetEnemy(enemyType, enemySpawner);

            asteroid.BulletHit += OnBulletHit;
        }
    }

    private void OnBulletHit(Asteroid asteroid)
    {
        asteroid.BulletHit -= OnBulletHit;

        if (asteroid.Type == EnemyType.LargeAsteroid)
        {
            GenerateWithSpawnStrategy(EnemyType.MediumAsteroid, 2, asteroid.AsteroidPartSpawner);
        }
        else if (asteroid.Type == EnemyType.MediumAsteroid)
        {
            GenerateWithSpawnStrategy(EnemyType.SmallAsteroid, 2, asteroid.AsteroidPartSpawner);
        }
    }
}
