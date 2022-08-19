using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSession
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private SelectorInput _selectorInput;
        [SerializeField] private Session _session;

        private Session _current;
        
        private void Start()
        {
            _current = FindObjectOfType<Session>();

            if (_current == null)
            {
                _current = Instantiate(_session);
                
                DontDestroyOnLoad(_current);
            }
            else
            {
                _selectorInput.SetInput(_current.SelectedInput);
                StartNewGame();
            }
        }
        
        private void OnEnable()
        {
            _gameCycle.GameEnded += OnGameEnded;
        }

        private void OnDisable()
        {
            _gameCycle.GameEnded -= OnGameEnded;
        }

        public void StartNewGame()
        {
            if (_gameCycle.IsActivity)
            {
                ReloadGame();
            }
            else
            {
                _gameCycle.Launch();
            }
        }

        public void ReloadGame()
        {
            _current.SelectedInput = _selectorInput.SelectedInputType;
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void OnGameEnded()
        {
            EventBus.Send<IGameOverListener>(listener => listener.OnGameOver());
        }
    }
}