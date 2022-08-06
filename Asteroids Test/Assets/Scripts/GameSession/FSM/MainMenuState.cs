namespace GameSession.FSM
{
    public class MainMenuState : IState
    {
        private IStateMachine _stateMachine;
        private MainMenuContext _mainMenuContext;
        
        public MainMenuState(IStateMachine stateMachine, MainMenuContext mainMenuContext)
        {
            _stateMachine = stateMachine;
            _mainMenuContext = mainMenuContext;
        }
        
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
            
        }
    }
}