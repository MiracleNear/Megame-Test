using System;

namespace CollisionInterface
{
    public interface IPlayerBulletCollisionHandler
    {
        public void OnCollisionPlayerBullet(Action<int> action);
    }
}