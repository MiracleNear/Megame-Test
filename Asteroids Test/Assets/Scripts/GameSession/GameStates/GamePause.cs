using UnityEngine;

namespace GameSession
{
    public class GamePause : MonoBehaviour, IPauseButtonClickListener
    {
        private bool _isPaused;

        private void Awake()
        {
            EventBus.Subscribe<IPauseButtonClickListener>(this);
        }

        private void OnDestroy()
        {
            EventBus.Subscribe<IPauseButtonClickListener>(this);

            PauseManager.GetInstance().Dispose();
        }

        public void OnClickPauseButton()
        {
            _isPaused = !_isPaused;
            
            PauseManager.GetInstance().SetPause(_isPaused);
        }
    }
}