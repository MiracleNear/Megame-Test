using UnityEngine;

namespace Spawner
{
    public interface IEnemyPlacer
    {
        public Vector2 GetPosition();

        public Vector2 GetDirectionFrom(Vector2 position);
    }
}