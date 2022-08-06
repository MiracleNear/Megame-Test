using GameSession.FSM;
using UnityEngine;

namespace GameSession
{
    public class Bootstrap : MonoBehaviour
    {
        [SerializeField] private GameCycleContext _gameCycleContext;
        [SerializeField] private GameOverContext _gameOverContext;
        [SerializeField] private MainMenuContext _mainMenuContext;

        private StateMachine _stateMachine;
        
        private void Awake()
        {
            _stateMachine = new StateMachine();
            
            _stateMachine.Register(new GameCycleState(_stateMachine, _gameCycleContext));
            _stateMachine.Register(new GameOverState(_stateMachine,  _gameOverContext));
            _stateMachine.Register(new MainMenuState(_stateMachine, _mainMenuContext));
        }

        private void Start()
        {
            _stateMachine.Enter<MainMenuState>();
        }
    }
}
