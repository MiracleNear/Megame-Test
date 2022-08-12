using System;
using Factories;

namespace Enemies
{
    public class AsteroidType
    {
        public bool IsPrimitive { get; private set; }
        public EnemyType NextPartAsteroid { get; private set; }
        
        public static AsteroidType ConvertEnemyTypeToAsteroidType(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.LargeAsteroid:
                    return new AsteroidType(EnemyType.MediumAsteroid, false);
                case EnemyType.MediumAsteroid:
                    return new AsteroidType(EnemyType.SmallAsteroid, false);
                case EnemyType.SmallAsteroid:
                    return new AsteroidType(EnemyType.SmallAsteroid,  true);
                default:
                    throw new Exception($"no such template for asteroid : {enemyType}");
            }
        }
        
        private AsteroidType(EnemyType nextPartAsteroid, bool isPrimitive)
        {
            IsPrimitive = isPrimitive;
            NextPartAsteroid = nextPartAsteroid;
        }
    }
}