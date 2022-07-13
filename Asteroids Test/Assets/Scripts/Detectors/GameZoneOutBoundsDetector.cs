using DefaultNamespace;
using UnityEngine;

namespace Detectors
{
    public enum ExitSide
    {
        None,
        Vertical,
        Horizontal,
    }
    
    public class GameZoneOutBoundsDetector
    {
        private Collider2D _collider;
        private Vector2 _halfScreenSize => ScreenBoundSize.HalfSize;

        public GameZoneOutBoundsDetector(Collider2D collider)
        {
            _collider = collider;
        }

        public bool IsOutBoundsGameZone(Vector2 position, out ExitSide exitSide)
        {
            Vector2 extentsCollider = _collider.bounds.extents;
            
            if (IsPositionOutBounds(position.x, _halfScreenSize.x, extentsCollider.x))
            {
                exitSide = ExitSide.Vertical;
                return true;
            }
            else if(IsPositionOutBounds(position.y, _halfScreenSize.y, extentsCollider.y))
            {
                exitSide = ExitSide.Horizontal;
                return true;
            }

            exitSide = ExitSide.None;
            return false;
        }
        
        private bool IsPositionOutBounds(float position, float halfScreenSize, float offsetByColliderExtents)
        {
            float displacedBorder = halfScreenSize + offsetByColliderExtents;

            if (position < -displacedBorder || position > displacedBorder)
            {
                return true;
            }

            return false;
        }
    }
}