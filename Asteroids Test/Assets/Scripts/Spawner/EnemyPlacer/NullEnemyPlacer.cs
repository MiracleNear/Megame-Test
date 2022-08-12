using UnityEngine;

namespace Spawner
{
    public class NullEnemyPlacer : IEnemyPlacer
    {
        public Vector2 GetPosition()
        {
            return Vector2.negativeInfinity;
        }

        public Vector2 GetDirectionFrom(Vector2 position)
        {
            return Vector2.negativeInfinity;
        }
    }
}