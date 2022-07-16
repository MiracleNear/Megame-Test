using System;
using DefaultNamespace.GameSession;
using UnityEngine;
using UnityEngine.UI;

public class GameStarter : MonoBehaviour
{
    public event Action GameLaunched;
    public bool IsGameStarted { get; private set; }
    
    [SerializeField] private Menu _menu;
    [SerializeField] private Button _gameLaunch;

    private SceneContext _sceneContext => SceneContext.Instance;
    
    private void OnEnable()
    {
        _gameLaunch.onClick.AddListener(GameLaunch);
    }

    private void Start()
    {
        IsGameStarted = _sceneContext.IsSceneReload;
        
        if (IsGameStarted)
        {
            _menu.Disable();

            GameLaunched?.Invoke();
        }
    }

    private void OnDisable()
    {
        _gameLaunch.onClick.RemoveListener(GameLaunch);
    }

    private void GameLaunch()
    {
        if (IsGameStarted)
        {
            _sceneContext.ReloadScene();
        }
        else
        {
            _menu.Disable();

            IsGameStarted = true;
        
            GameLaunched?.Invoke();
        }
    }
}
