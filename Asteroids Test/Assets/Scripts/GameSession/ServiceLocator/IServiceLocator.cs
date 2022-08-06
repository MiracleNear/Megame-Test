namespace GameSession.ServiceLocator
{
    public interface IServiceLocator
    {
        public T GetService<T>();
    }
}