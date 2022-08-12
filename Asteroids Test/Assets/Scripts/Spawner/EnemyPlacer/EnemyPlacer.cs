using Enemies;
using UnityEngine;

namespace Spawner
{
    public class EnemyPlacer
    {
        public static IEnemyPlacer CreateAsteroidPlacer(Collider2D asteroidCollider)
        {
            return new AsteroidPlacer(asteroidCollider);
        }

        public static IEnemyPlacer CreateAsteroidPartPlacer(Asteroid asteroid, float yawAngle)
        {
            return new AsteroidPartPlacer(asteroid, yawAngle);
        }

        public static IEnemyPlacer CreateUfoPlacer(Collider2D ufoCollider, float offsetFromTheVerticalBorderInPercent)
        {
            return new UfoPlacer(ufoCollider, offsetFromTheVerticalBorderInPercent);
        }

        public static IEnemyPlacer CreateNullPlacer()
        {
            return new NullEnemyPlacer();
        }
        
        private EnemyPlacer()
        {
            
        }
    }
}