using GameSession;
using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    public class MainMenu : MonoBehaviour, IGameStartListener, IGameOverListener, IPauseHandler
    {
        [SerializeField] private Button _resumeGame;
        [SerializeField] private GameObject _mainScreen;
        [SerializeField] private GameObject _gameOverScreen;

        private Image _resumeGameButtonView;
        private Color _resumeButtonColorOnPause;

        private void Awake()
        {
            _resumeGameButtonView = _resumeGame.GetComponent<Image>();

            _resumeButtonColorOnPause = new Color(_resumeGameButtonView.color.r, _resumeGameButtonView.color.g,
                _resumeGameButtonView.color.b, 1f);
            
            EventBus.Subscribe<IGameStartListener>(this);
            EventBus.Subscribe<IGameOverListener>(this);
        }

        private void Start()
        {
            PauseManager.GetInstance().Register(this);
        }

        private void OnEnable()
        {
            _resumeGame.onClick.AddListener(OnClickResumeButton);
        }

        private void OnDisable()
        {
            _resumeGame.onClick.RemoveListener(OnClickResumeButton);
        }

        private void OnDestroy()
        {
            EventBus.UnSubscribe<IGameStartListener>(this);
            EventBus.UnSubscribe<IGameOverListener>(this);
        }

        public void OnStartGame()
        {
            _mainScreen.SetActive(false);
        }

        public void OnGameOver()
        {
            _gameOverScreen.SetActive(true);
        }

        public void SetPause(bool isPaused)
        {
            _mainScreen.SetActive(isPaused);
            
            if (isPaused)
            {
                if (!_resumeGame.enabled)
                {
                    _resumeGame.enabled = true;

                    _resumeGameButtonView.color = _resumeButtonColorOnPause;
                }
            }
        }

        private void OnClickResumeButton()
        {
            EventBus.Send<IPauseButtonClickListener>(listener => listener.OnClickPauseButton());
        }
    }
}