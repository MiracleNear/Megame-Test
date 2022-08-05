using DefaultNamespace.Weapon;
using ObjectPools;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(UfoBulletPool))]
    public class UfoBulletFactory : BulletFactory<UfoBullet>
    {
        protected override void InitFactory()
        {
            _bulletPool = GetComponent<UfoBulletPool>();
        }

        protected override UfoBullet Init(UfoBullet bullet)
        {
            return bullet;
        }
    }
}