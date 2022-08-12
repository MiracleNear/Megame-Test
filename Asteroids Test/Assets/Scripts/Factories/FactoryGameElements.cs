using UnityEngine;

namespace Factories
{
    public abstract class FactoryGameElements<T> : MonoBehaviour, IFactoryGameElements where T : Component
    {
        public void Recycle(Component element)
        {
            Reclaim(element as T);
        }
        
        
        protected abstract void Reclaim(T element);
    }
}