using Factories;
using Spawner;

namespace Enemies
{
    public class PartAsteroid : AsteroidType
    {
        private readonly AsteroidSpawner _asteroidSpawner;
        private readonly float _yawAngle;
        private readonly EnemyType _nextPartAsteroid;

        public PartAsteroid(AsteroidSpawner asteroidSpawner, float yawAngle, EnemyType nextPartAsteroid)
        {
            _asteroidSpawner = asteroidSpawner;
            _yawAngle = yawAngle;
            _nextPartAsteroid = nextPartAsteroid;
        }

        public override void CreatePartAsteroidsFor(Asteroid asteroid)
        {
            _asteroidSpawner.SpawnPartAsteroids(EnemyPlacer.CreateAsteroidPartPlacer(asteroid, _yawAngle), _nextPartAsteroid);
        }
    }
}