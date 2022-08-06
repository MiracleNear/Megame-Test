using ObjectPools;
using UnityEngine;

namespace Factories
{
    public abstract class BulletFactory<T> : MonoBehaviour, IBulletFactory where T : Bullet 
    {
        protected IObjectPool<T> BulletPool;

        private void Awake()
        {
            InitFactory();
        }

        public void Create(Vector2 position, Vector3 direction)
        {
            Bullet bulletElement = BulletPool.GetFreeElement();

            bulletElement.OriginFactory = this;

            Init((T) bulletElement);
            
            bulletElement.Init(position, direction);
        }
        
        public void Reclaim(Bullet bullet)
        {
            BulletPool.ReturnToPool(bullet as T);
        }

        protected abstract void InitFactory();

        protected abstract T Init(T bullet);
    }
}