using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GamePause _gamePause;
    [SerializeField] private GameObject[] _pauseMenuItemsToEnable;
    
    private void OnEnable()
    {
        _gamePause.PausePressed += OnPausePressed;
        _gamePause.PauseUnpressed += OnPauseUnpressed;
    }

    private void OnDisable()
    {
        _gamePause.PausePressed -= OnPausePressed;
        _gamePause.PauseUnpressed -= OnPauseUnpressed;
    }

    public void Enable()
    {
        SetActive(true);
    }

    public void Disable()
    {
        SetActive(false);
    }
    
    private void OnPauseUnpressed()
    {
        SetActive(false);
    }

    private void OnPausePressed()
    {
        SetActive(true);
    }

    private void SetActive(bool isActive)
    {
        foreach (GameObject pauseMenuItem in _pauseMenuItemsToEnable)
        {
            pauseMenuItem.SetActive(isActive);
        }
    }
}
