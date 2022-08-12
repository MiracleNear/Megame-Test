using UnityEngine;

namespace ObjectPools
{
    public interface IObjectPool<T> where T : Component
    {
        public T GetFreeElement();
        public void ReturnToPool(T enemy);
    }
}