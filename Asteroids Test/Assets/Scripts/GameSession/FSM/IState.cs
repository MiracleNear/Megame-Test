namespace GameSession.FSM
{
    public interface IState
    {
        public void OnEnter();
        public void OnExit();
    }
}