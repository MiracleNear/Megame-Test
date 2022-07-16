using System;
using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.UI;

public class GamePause : MonoBehaviour
{
    public event Action PausePressed;

    public event Action PauseUnpressed;
    
    [SerializeField] private InputHandler _inputHandler;
    [SerializeField] private Button _continueGameButton;

    private PauseManager _pauseManager => SceneContext.Instance.PauseManager;
    
    private void OnEnable()
    {
        _inputHandler.PauseButtonPressed += OnPauseButtonPressed;
        _continueGameButton.onClick.AddListener(OnPauseButtonUnpressed);
    }

    private void OnDisable()
    {
        _inputHandler.PauseButtonPressed -= OnPauseButtonPressed;
        _continueGameButton.onClick.AddListener(OnPauseButtonPressed);
    }

    private void OnPauseButtonPressed()
    {
        if (!_pauseManager.IsPaused)
        {
            _pauseManager.SetPaused(true);
            
            PausePressed?.Invoke();
        }
    }

    private void OnPauseButtonUnpressed()
    {
        if (_pauseManager.IsPaused)
        {
            _pauseManager.SetPaused(false);
            
            PauseUnpressed?.Invoke();
        }
    }
}