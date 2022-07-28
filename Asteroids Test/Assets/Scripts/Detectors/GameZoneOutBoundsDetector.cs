using DefaultNamespace;
using UnityEngine;

namespace Detectors
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameZoneOutBoundsDetector : MonoBehaviour
    {
        private bool _isPreviousPositionInGameZone;
        private Vector2 _displacedBorder;

        private void Awake()
        {
            _displacedBorder = ScreenBoundSize.HalfSize + (Vector2)GetComponent<BoxCollider2D>().bounds.extents;
        }

        private void OnDisable()
        {
            _isPreviousPositionInGameZone = false;
        }
        
        private void LateUpdate()
        {
            TryReflectPosition(transform.position);
        }

        private void TryReflectPosition(Vector2 position)
        {
            if (_isPreviousPositionInGameZone)
            {
                if (IsPositionOutBounds(position.x, _displacedBorder.x)) position.x = -position.x;

                if (IsPositionOutBounds(position.y, _displacedBorder.y)) position.y = -position.y;
            }

            transform.position = position;
            
            _isPreviousPositionInGameZone = IsPositionInBounds();
        }
        
        private bool IsPositionOutBounds(float position, float displacedBorderPosition)
        {
            return (position < -displacedBorderPosition || position > displacedBorderPosition) && _isPreviousPositionInGameZone;
        }

        private bool IsPositionInBounds()
        {
            Vector2 position = transform.position;
            
            return position.x >= -_displacedBorder.x && position.x <= _displacedBorder.x &&
                   position.y >= -_displacedBorder.y && position.y <= _displacedBorder.y;
        }
    }
}