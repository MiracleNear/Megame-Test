using Enemies;
using Spawner;
using UnityEngine;

namespace Factories
{
    public class AsteroidPartPlacerFactory : EnemyPlacerFactory
    {
        [SerializeField] private float _yawAngle;
        [SerializeField] private Asteroid _asteroid;

        public override IEnemyPlacer Get()
        {
           AsteroidPartPlacer asteroidPartPlacer = new AsteroidPartPlacer(_asteroid, _yawAngle);

           return asteroidPartPlacer;
        }
    }
}