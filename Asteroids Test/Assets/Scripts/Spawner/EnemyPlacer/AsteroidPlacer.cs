using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class AsteroidPlacer : IEnemyPlacer
	{
        private Collider2D _asteroidCollider;
        private float _baseHorizontalPosition => ScreenBoundSize.HalfSize.x;
        private float _baseVerticalPosition => ScreenBoundSize.HalfSize.y;

        public AsteroidPlacer(Collider2D asteroidCollider)
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
            return Random.value >= 0.5f ? GetRandomHorizontalPosition() : GetRandomVerticalPosition();
        }

        private Vector2 GetRandomVerticalPosition()
        {
            Vector2 Vector = new Vector2(GetRandomOffsetPosition(_baseHorizontalPosition, _asteroidCollider.bounds.size.x), 
                Random.Range(-_baseVerticalPosition, _baseVerticalPosition));
            
            return Vector;
        }
        
        private Vector2 GetRandomHorizontalPosition()
        { 
            Vector2 Vector = new Vector2(Random.Range(-_baseHorizontalPosition, _baseHorizontalPosition), 
                GetRandomOffsetPosition(_baseVerticalPosition, _asteroidCollider.bounds.size.y));
            
            return Vector;
        }
        
        private float GetRandomOffsetPosition(float position, float offset)
        {
            float sign = Random.value >= 0.5f ? 1f : -1f;
            
            
            return position + sign * offset;
        }
    }
}