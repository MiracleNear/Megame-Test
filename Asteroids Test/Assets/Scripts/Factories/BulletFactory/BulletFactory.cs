using ObjectPools;
using UnityEngine;
using WeaponSystem;

namespace Factories
{
    public abstract class BulletFactory<T> : FactoryGameElements<T>, IBulletFactory where T : Bullet
    {
        protected IObjectPool<T> BulletPool;

        private void Awake()
        {
            InitFactory();
        }

        public void Create(Vector2 position, Vector3 direction)
        {
            Bullet bulletElement = BulletPool.GetFreeElement();

            bulletElement.RecyclerFactoryGameElements = this;

            Init((T) bulletElement);
            
            bulletElement.Init(position, direction);
        }

        protected override void Reclaim(T element)
        {
            BulletPool.ReturnToPool(element);
        }


        protected abstract void InitFactory();

        protected abstract T Init(T bullet);
        
    }
}