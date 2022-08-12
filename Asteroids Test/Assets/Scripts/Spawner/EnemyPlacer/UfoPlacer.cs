using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class UfoPlacer : IEnemyPlacer
	{
        private readonly BoxCollider2D _ufoCollider;
        private readonly float _offsetFromTheVerticalBorderInPercent;
        private float _baseHorizontalPosition => ScreenBoundSize.HalfSize.x;
        private float _baseVerticalPosition => ScreenBoundSize.HalfSize.y;
        
        public UfoPlacer(BoxCollider2D ufoCollider, float offsetFromTheVerticalBorderInPercent)
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
            return Random.Range(-GetOffsetFromTheVerticalBorder(), GetOffsetFromTheVerticalBorder());
        }

        private float GetRandomHorizontalPosition()
        {
            return Mathf.Sign(Random.Range(-1, 1)) * (_baseHorizontalPosition + _ufoCollider.size.x);
        }
        
        private float GetOffsetFromTheVerticalBorder()
        {
            return _baseVerticalPosition - (_baseVerticalPosition/ 100f) * _offsetFromTheVerticalBorderInPercent;
        }
    }
}