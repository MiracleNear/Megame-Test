using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameSession
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameCycle _gameCycle;
        [SerializeField] private SelectorInput _selectorInput;

        private Session _session;

        private void Start()
        {
            _session = FindObjectOfType<Session>();

            if (_session == null)
            {
                _session = new GameObject("Session Info").AddComponent<Session>();

                DontDestroyOnLoad(_session);
            }
            else
            {
                _selectorInput.SetInput(_session.SelectedInput);
                
                StartGame();
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

        public void StartGame()
        {
            if (_gameCycle.IsActivity)
            {
                _session.SelectedInput = _selectorInput.SelectedInputType;
                
                ReloadGame();
            }
            else
            {
                _gameCycle.Launch();
            }
        }

        public void ExitGame()
        {
            Application.Quit();
        }

        private void ReloadGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        
        private void OnGameEnded()
        {
            EventBus.Send<IGameOverListener>(listener => listener.OnGameOver());
        }
    }
}