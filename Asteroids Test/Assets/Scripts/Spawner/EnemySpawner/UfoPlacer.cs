using DefaultNamespace;
using UnityEngine;

namespace Spawner
{
    public class UfoPlacer : IEnemyPlacer
    {
        private float _maxVerticalPositionSpawn;
        private float _minVerticalPositionSpawn;
        private float _leftHorizontalPosition;
        private float _rightHorizontalPosition;
        
        public UfoPlacer(Bounds ufoBounds, float offsetFromTheVerticalBorderInPercent)
        {
            float offsetFromTheVerticalBorder =
                (ScreenBoundSize.HalfSize.y / 100f) * offsetFromTheVerticalBorderInPercent;

            _maxVerticalPositionSpawn = ScreenBoundSize.HalfSize.y - offsetFromTheVerticalBorder;
            _minVerticalPositionSpawn = -_maxVerticalPositionSpawn;
            
            float horizontalPosition = ScreenBoundSize.HalfSize.x + ufoBounds.size.x;

            _leftHorizontalPosition = -horizontalPosition;
            _rightHorizontalPosition = horizontalPosition;
        }
        
        public Vector2 GetPosition()
        {
            float randomVerticalPosition = Random.Range(_minVerticalPositionSpawn, _maxVerticalPositionSpawn);

            return Random.value >= 0.5f
                ? new Vector2(_rightHorizontalPosition, randomVerticalPosition)
                : new Vector2(_leftHorizontalPosition, randomVerticalPosition);
        }

        public Vector2 GetDirectionFrom(Vector2 position)
        {
            return position.x > 0 ? Vector2.left : Vector2.right;
        }
    }
}