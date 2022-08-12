using System;

namespace Enemies
{
    public interface IEnemy<T>
    {
        public event Action<T> Died;
    }
}