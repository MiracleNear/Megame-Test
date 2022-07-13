using Detectors;
using UnityEngine;

namespace Handlers
{
    public enum ObjectLocationStatus
    {
        InBounds,
        OutBounds,
    }
    
    public class GameZoneOutBoundsHandler : MonoBehaviour
    {
        private GameZoneOutBoundsDetector _gameZoneOutBoundsDetector;
        private ObjectLocationStatus _previousLocationStatus;
        
        private void Awake()
        {
            _previousLocationStatus = ObjectLocationStatus.OutBounds;

            _gameZoneOutBoundsDetector = new GameZoneOutBoundsDetector(GetComponent<Collider2D>());
        }

        private void Update()
        {
            if (_gameZoneOutBoundsDetector.IsOutBoundsGameZone(transform.position, out ExitSide exitSide) && _previousLocationStatus == ObjectLocationStatus.InBounds)
            {
                HandleObjectOutBounds(exitSide);
            }

            _previousLocationStatus = exitSide == ExitSide.None ? ObjectLocationStatus.InBounds : ObjectLocationStatus.OutBounds;
        }

        private void HandleObjectOutBounds(ExitSide exitSide)
        {
            switch (exitSide)
            {
                case ExitSide.Horizontal:
                    transform.position = new Vector3(transform.position.x, -transform.position.y);
                    break;
                case ExitSide.Vertical:
                    transform.position = new Vector3(-transform.position.x, transform.position.y);
                    break;
            }
        }
    }
}