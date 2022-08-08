namespace GameSession
{
    public interface IGameStartListener : IGlobalEvent
    {
        public void OnStartGame();
    }
}