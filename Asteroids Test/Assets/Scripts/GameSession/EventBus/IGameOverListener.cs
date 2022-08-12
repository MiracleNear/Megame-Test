namespace GameSession
{
    public interface IGameOverListener : IGlobalEvent
    {
        public void OnGameOver();
    }
}