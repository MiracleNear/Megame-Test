using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    public class AsteroidPartSpawner : EnemySpawner
    {
        [SerializeField] private Asteroid _asteroid;
        [SerializeField] private float _yawAngle;

        private Quaternion _rotation;
        protected override void InitSpawner()
        {
            _rotation = Quaternion.AngleAxis(_yawAngle, Vector3.forward);
        }

        protected override Vector2 GetPosition()
        {
            return _asteroid.transform.position;
        }

        protected override Vector2 GetDirectionFrom(Vector2 position)
        {
            Vector2 direction = _rotation * _asteroid.Direction;

            _rotation = Quaternion.Inverse(_rotation);

            return direction;
        }
    }
}