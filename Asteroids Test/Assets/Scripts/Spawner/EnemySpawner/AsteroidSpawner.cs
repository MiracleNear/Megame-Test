using DefaultNamespace;
using Enemies;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class AsteroidSpawner : EnemySpawner
	{
        [SerializeField] private Collider2D _asteroidCollider;

        private float _minHorizontalPosition, _maxHorizontalPosition;
        private float _minVerticalPosition, _maxVerticalPosition;

        protected override void InitSpawner()
        {
            _maxHorizontalPosition = ScreenBoundSize.HalfSize.x + _asteroidCollider.bounds.size.x;
            _maxVerticalPosition = ScreenBoundSize.HalfSize.y + _asteroidCollider.bounds.size.y;

            _minHorizontalPosition = -_maxHorizontalPosition;
            _maxVerticalPosition = -_minVerticalPosition;
        }

        protected override Vector2 GetPosition()
        {
            return GetRandomPosition();
        }

        protected override Vector2 GetDirectionFrom(Vector2 position)
        {
            return (Random.insideUnitCircle - position).normalized;
        }

        private Vector2 GetRandomPosition()
        {
            return Random.value >= 0.5f ? GetRandomHorizontalPosition() : GetRandomVerticalPosition();
        }

        private Vector2 GetRandomVerticalPosition()
        {
            float randomVerticalPosition = Random.Range(_minVerticalPosition, _maxVerticalPosition);

            return Random.value >= 0.5f
                ? new Vector2(_maxHorizontalPosition, randomVerticalPosition)
                : new Vector2(_minHorizontalPosition, randomVerticalPosition);
        }

        private Vector2 GetRandomHorizontalPosition()
        {
            float randomHorizontalPosition = Random.Range(_minHorizontalPosition, _maxHorizontalPosition);

            return Random.value >= 0.5f
                ? new Vector2(randomHorizontalPosition, _maxVerticalPosition)
                : new Vector2(randomHorizontalPosition, _minVerticalPosition);
        }
    }
}