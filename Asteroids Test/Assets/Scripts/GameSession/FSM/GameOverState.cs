namespace GameSession.FSM
{
    public class GameOverState : IState
    {
        private IStateMachine _stateMachine;
        private GameOverContext _gameOverContext;
        
        public GameOverState(IStateMachine stateMachine, GameOverContext gameOverContext)
        {
            _stateMachine = stateMachine;
            _gameOverContext = gameOverContext;
        }
        
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}