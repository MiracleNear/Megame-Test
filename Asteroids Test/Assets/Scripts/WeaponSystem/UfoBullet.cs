using CollisionInterface;
using UnityEngine;

namespace WeaponSystem
{
    public class UfoBullet : Bullet
    {
        protected override bool TryCollisionWith(GameObject gameObject)
        {
            if (gameObject.TryGetComponent(out IUfoBulletCollisionHandler bulletCollisionHandler))
            {
                bulletCollisionHandler.OnCollisionUfoBullet();
                return true;
            }

            return false;
        }
        
    }
}
