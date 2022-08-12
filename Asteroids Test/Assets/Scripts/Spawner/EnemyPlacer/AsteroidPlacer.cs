using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class AsteroidPlacer : IEnemyPlacer
	{
        private BoxCollider2D _asteroidCollider;
        private float _baseHorizontalPosition => ScreenBoundSize.HalfSize.x;
        private float _baseVerticalPosition => ScreenBoundSize.HalfSize.y;

        public AsteroidPlacer(BoxCollider2D asteroidCollider)
        {
            _asteroidCollider = asteroidCollider;
        }

        public Vector2 GetPosition()
        {
            return GetRandomPosition();
        }

        public Vector2 GetDirectionFrom(Vector2 position)
        {
            return (Random.insideUnitCircle - position).normalized;
        }

        private Vector2 GetRandomPosition()
        {
            return Random.Range(0, 2) == 0 ? GetRandomHorizontalPosition() : GetRandomVerticalPosition();
        }

        private Vector2 GetRandomVerticalPosition()
        {
            return new Vector2(GetRandomOffsetPosition(_baseHorizontalPosition, _asteroidCollider.size.x),
                Random.Range(-_baseVerticalPosition, _baseVerticalPosition));
        }
        
        private Vector2 GetRandomHorizontalPosition()
        {
            return new Vector2(Random.Range(-_baseHorizontalPosition, _baseHorizontalPosition),
                GetRandomOffsetPosition(_baseVerticalPosition, _asteroidCollider.size.y));
        }
        
        private float GetRandomOffsetPosition(float position, float offset)
        {
            float sign = Mathf.Sign(Random.Range(-1, 1));
            
            return sign * (position +  offset);
        }
    }
}