using DefaultNamespace;
using Enemies;
using Factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class AsteroidPlacer : IEnemyPlacer
	{
        private float _minHorizontalPosition, _maxHorizontalPosition;
        private float _minVerticalPosition, _maxVerticalPosition;

        public AsteroidPlacer(Collider2D asteroidCollider)
        {
            _maxHorizontalPosition = ScreenBoundSize.HalfSize.x + asteroidCollider.bounds.size.x;
            _maxVerticalPosition = ScreenBoundSize.HalfSize.y + asteroidCollider.bounds.size.y;
            
            Debug.DrawLine(Vector3.zero, new Vector3(_maxHorizontalPosition, 0f), Color.red, 5f);
            Debug.DrawLine(Vector3.zero, new Vector3(0f, _maxVerticalPosition), Color.red, 5f);
            
            _minHorizontalPosition = -_maxHorizontalPosition;
            _minVerticalPosition = -_maxVerticalPosition;
        }

        public Vector2 GetPosition()
        {
            return GetRandomPosition();
        }

        public  Vector2 GetDirectionFrom(Vector2 position)
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