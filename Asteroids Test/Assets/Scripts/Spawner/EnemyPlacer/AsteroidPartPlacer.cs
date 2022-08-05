using Enemies;
using UnityEngine;

namespace Spawner
{
    public class AsteroidPartPlacer : IEnemyPlacer
    {
        private Asteroid _asteroid;
        private Quaternion _rotation;
        
        public AsteroidPartPlacer(Asteroid asteroid, float yawAngle)
        {
            _asteroid = asteroid;
            _rotation = Quaternion.AngleAxis(yawAngle, Vector3.forward);
        }

        public  Vector2 GetPosition()
        {
            return _asteroid.transform.position;
        }

        public Vector2 GetDirectionFrom(Vector2 position)
        {
            Vector2 direction = _rotation * _asteroid.Direction;

            _rotation = Quaternion.Inverse(_rotation);

            return direction;
        }
    }
}