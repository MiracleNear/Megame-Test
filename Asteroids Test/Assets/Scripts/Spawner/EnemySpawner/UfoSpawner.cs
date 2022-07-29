using System.Collections;
using DefaultNamespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
	public class UfoSpawner : EnemySpawner
	{
        [SerializeField] private Collider2D _ufoCollider;
        [SerializeField] private float _offsetFromTheVerticalBorderInPercent;

        private float _maxVerticalPositionSpawn;
        private float _minVerticalPositionSpawn;
        private float _leftHorizontalPosition;
        private float _rightHorizontalPosition;

        protected override void InitSpawner()
        {
            float offsetFromTheVerticalBorder =
                (ScreenBoundSize.HalfSize.y / 100f) * _offsetFromTheVerticalBorderInPercent;

            _maxVerticalPositionSpawn = ScreenBoundSize.HalfSize.y - offsetFromTheVerticalBorder;
            _minVerticalPositionSpawn = -_maxVerticalPositionSpawn;

            float horizontalPosition = ScreenBoundSize.HalfSize.x + _ufoCollider.bounds.size.x;

            _leftHorizontalPosition = -horizontalPosition;
            _rightHorizontalPosition = horizontalPosition;

        }
        protected override Vector2 GetPosition()
        {
            float randomVerticalPosition = Random.Range(_minVerticalPositionSpawn, _maxVerticalPositionSpawn);

            return Random.value >= 0.5f
                ? new Vector2(_rightHorizontalPosition, randomVerticalPosition)
                : new Vector2(_leftHorizontalPosition, randomVerticalPosition);
        }

        protected override Vector2 GetDirectionFrom(Vector2 position)
        {
            return position.x > 0 ? Vector2.left : Vector2.right;
        }
    }
}