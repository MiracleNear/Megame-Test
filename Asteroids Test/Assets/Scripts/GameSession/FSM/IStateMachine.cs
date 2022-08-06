namespace GameSession.FSM
{
    public interface IStateMachine
    {
        public void Enter<T>() where T : IState;
    }
}