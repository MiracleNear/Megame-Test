using ObjectPools;
using UnityEngine;

namespace Factories
{
    public enum BulletType
	{
        Player,
        Ufo,
	}

    [RequireComponent(typeof(BulletPool))]
    public class BulletFactory : MonoBehaviour
    {
        private IObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = GetComponent<BulletPool>();
        }

        public void Create(Vector2 position, Vector3 direction, Color color, BulletType bulletType)
        {
            Bullet bullet = _bulletPool.GetFreeElement();

            bullet.OriginFactory = this;

            bullet.Init(position, direction, color, bulletType);
        }


        public void Reclaim(Bullet bullet)
        {
            _bulletPool.ReturnToPool(bullet);
        }
    }
}