using System;
using DefaultNamespace.GameSession;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action PauseButtonPressed;

    [SerializeField] private GameStarter _gameStarter;
    
    private Player _player;
    private IInput _input;
        
    public void BindPlayer(Player player)
    {
        _player = player;
    }

    public void SetInput(IInput input)
    {
        _input = input;
    }
        
    private void Update()
    {
        if(!_gameStarter.IsGameStarted) return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseButtonPressed?.Invoke();
        }

        if (_player != null)
        {
            _input.Update(_player);

            _player.Move();
        }
    }
}