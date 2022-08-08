using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameSession
{
    public class MainMenu : MonoBehaviour, IGameStartListener, IPauseHandler
    {
        [SerializeField] private Button _resumeGame;

        private Image _resumeGameButtonView;
        private Color _resumeButtonColorOnPause;

        private void Awake()
        {
            _resumeGameButtonView = _resumeGame.GetComponent<Image>();

            _resumeButtonColorOnPause = new Color(_resumeGameButtonView.color.r, _resumeGameButtonView.color.g,
                _resumeGameButtonView.color.b, 1f);
            
            EventBus.Subscribe<IGameStartListener>(this);
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
        }

        public void OnStartGame()
        {
            gameObject.SetActive(false);
        }

        public void SetPause(bool isPaused)
        {
            if (isPaused)
            {
                gameObject.SetActive(true);

                if (!_resumeGame.enabled)
                {
                    _resumeGame.enabled = true;

                    _resumeGameButtonView.color = _resumeButtonColorOnPause;
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void OnClickResumeButton()
        {
            EventBus.Send<IPauseButtonClickListener>(listener => listener.OnClickPauseButton());
        }
    }
}