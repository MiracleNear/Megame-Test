using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class UfoPlacer : IEnemyPlacer
	{
        private Collider2D _ufoCollider;
        private float _offsetFromTheVerticalBorderInPercent;
        private float _baseHorizontalPosition => ScreenBoundSize.HalfSize.x;
        private float _baseVerticalPosition => ScreenBoundSize.HalfSize.y;
        
        public UfoPlacer(Collider2D ufoCollider, float offsetFromTheVerticalBorderInPercent)
        {
            _offsetFromTheVerticalBorderInPercent = offsetFromTheVerticalBorderInPercent;
            _ufoCollider = ufoCollider;
        }
        
        public Vector2 GetPosition()
        {
            return new Vector2(GetRandomHorizontalPosition(), GetRandomVerticalPositionSpawn());
        }

        public Vector2 GetDirectionFrom(Vector2 position)
        {
            return position.x > 0 ? Vector2.left : Vector2.right;
        }

        private float GetRandomVerticalPositionSpawn()
        {
            float minPosition = GetOffsetPosition(_baseHorizontalPosition, GetOffsetFromTheVerticalBorder(), -1);
            float maxPosition = GetOffsetPosition(_baseHorizontalPosition, GetOffsetFromTheVerticalBorder(), 1);

            return Random.Range(minPosition, maxPosition);
        }

        private float GetRandomHorizontalPosition()
        {
            return GetOffsetPosition(_baseVerticalPosition, _ufoCollider.bounds.size.x, Mathf.Sign(Random.Range(-1, 1)));
        }

        private float GetOffsetPosition(float position, float offset, float sign)
        {
            return position + sign * offset;
        }

        private float GetOffsetFromTheVerticalBorder()
        {
            return (ScreenBoundSize.HalfSize.y / 100f) * _offsetFromTheVerticalBorderInPercent;
        }
    }
}