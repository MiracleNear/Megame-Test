using System;
using Spawner;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action PauseButtonPressed;

    [SerializeField] private GameStarter _gameStarter;
    [SerializeField] private PlayerSpawner _playerSpawner;
    
    private Player _player;
    private IInput _input;

    private void OnEnable()
    {
        _playerSpawner.Spawned += BindPlayer;
    }

    private void OnDisable()
    {
        _playerSpawner.Spawned -= BindPlayer;
    }

    public void SetInput(IInput input)
    {
        _input = input;
    }

    private void BindPlayer(Player player)
    {
        _player = player;
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