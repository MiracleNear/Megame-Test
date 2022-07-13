namespace ObjectPools
{
    public interface IObjectPool<T>
    {
        public T GetFreeElement();
        public void ReturnToPool(T element);
    }
}