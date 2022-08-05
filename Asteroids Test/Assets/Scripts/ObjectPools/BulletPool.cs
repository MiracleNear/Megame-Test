using Factories;
using UnityEngine;

namespace ObjectPools
{
    public class BulletPool : ObjectPool<Bullet>
    {
        [SerializeField] private BulletFactory _bulletFactory;

        public void Get()
        {
            
        }
    }
}