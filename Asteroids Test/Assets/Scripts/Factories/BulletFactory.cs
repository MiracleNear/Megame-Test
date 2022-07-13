using ObjectPools;
using UnityEngine;

namespace Factories
{
    [RequireComponent(typeof(BulletPool))]
    public class BulletFactory : MonoBehaviour
    {
        private IObjectPool<Bullet> _bulletPool;

        private void Awake()
        {
            _bulletPool = GetComponent<BulletPool>();
        }

        public void Create(Vector2 position, Vector3 direction, string layerMaskName, Color color)
        {
            Bullet bullet = _bulletPool.GetFreeElement();

            bullet.OriginFactory = this;
            
            bullet.Init(position, direction, color, layerMaskName);
        }

        public void Reclaim(Bullet bullet)
        {
            _bulletPool.ReturnToPool(bullet);
        }
    }
}