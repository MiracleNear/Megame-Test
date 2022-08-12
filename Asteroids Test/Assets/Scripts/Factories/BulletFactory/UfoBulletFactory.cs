using ObjectPools;
using UnityEngine;
using WeaponSystem;

namespace Factories
{
    [RequireComponent(typeof(UfoBulletPool))]
    public class UfoBulletFactory : BulletFactory<UfoBullet>
    {
        protected override void InitFactory()
        {
            BulletPool = GetComponent<UfoBulletPool>();
        }

        protected override UfoBullet Init(UfoBullet bullet)
        {
            return bullet;
        }
    }
}