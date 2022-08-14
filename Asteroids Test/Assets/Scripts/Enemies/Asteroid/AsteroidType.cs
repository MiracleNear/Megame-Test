using System;
using Factories;
using Spawner;

namespace Enemies
{
    public abstract class AsteroidType
    {
        public static AsteroidType CreateAsteroidType(AsteroidSpawner asteroidSpawner, float yawAngle, EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.LargeAsteroid:
                    return new PartAsteroid(asteroidSpawner, yawAngle, EnemyType.MediumAsteroid);
                case EnemyType.MediumAsteroid:
                    return new PartAsteroid(asteroidSpawner, yawAngle,EnemyType.SmallAsteroid);
                case EnemyType.SmallAsteroid:
                    return new PrimitiveAsteroid();
                default:
                    throw new Exception($"no such template for asteroid type {enemyType}");
            }
        }

        public abstract void CreatePartAsteroidsFor(Asteroid asteroid);
    }
}