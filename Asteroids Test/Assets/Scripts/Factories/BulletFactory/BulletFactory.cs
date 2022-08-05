using ObjectPools;
using UnityEngine;

namespace Factories
{
    public abstract class BulletFactory<T> : MonoBehaviour, IBulletFactory where T : Bullet 
    {
        protected IObjectPool<T> _bulletPool;

        private void Awake()
        {
            InitFactory();
        }

        public void Create(Vector2 position, Vector3 direction, Color color)
        {
            Bullet bulletElement = _bulletPool.GetFreeElement();

            bulletElement.OriginFactory = this;

            bulletElement.Init(position, direction, color);
        }
        
        public void Reclaim(Bullet bullet)
        {
            _bulletPool.ReturnToPool(bullet as T);
        }

        protected abstract void InitFactory();

        protected abstract T Init(T bullet);
    }
}