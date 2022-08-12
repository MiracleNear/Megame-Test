using UnityEngine;

namespace Factories
{
    public interface IFactoryGameElements
    {
        public void Recycle(Component element);
    }
}