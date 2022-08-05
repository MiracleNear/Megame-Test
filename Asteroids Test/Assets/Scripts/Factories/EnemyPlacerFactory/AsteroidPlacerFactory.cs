using Spawner;
using UnityEngine;

namespace Factories
{
    public class AsteroidPlacerFactory : EnemyPlacerFactory
    {
        [SerializeField] private Collider2D _asteroidCollider;

        public override IEnemyPlacer Get()
        {
            AsteroidPlacer asteroidPlacer = new AsteroidPlacer(_asteroidCollider);

            return asteroidPlacer;
        }
    }
}