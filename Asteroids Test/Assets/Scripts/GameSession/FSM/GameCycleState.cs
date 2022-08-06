namespace GameSession.FSM
{
    public class GameCycleState : IState
    {
        private IStateMachine _stateMachine;
        private GameCycleContext _gameCycleContext;
        public GameCycleState(IStateMachine stateMachine, GameCycleContext gameCycleContext)
        {
            _stateMachine = stateMachine;
            _gameCycleContext = gameCycleContext;
        }
        
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}