using UnityEngine;

namespace Factories
{
    public interface IBulletFactory
    {
        public void Create(Vector2 position, Vector3 direction);
    }
}