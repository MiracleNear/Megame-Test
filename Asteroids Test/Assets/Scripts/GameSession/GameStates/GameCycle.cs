using System;
using Spawner;
using UnityEngine;

namespace GameSession
{
    public class GameCycle : MonoBehaviour
    {
        public event Action GameEnded;
        
        public bool IsActivity { get; private set; }
        
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private PlayerLife _playerLife;

        private void OnEnable()
        {
            _playerSpawner.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            _playerSpawner.Spawned -= OnSpawned;
        }

        public void Launch()
        {
            _playerSpawner.InitialSpawn();

            IsActivity = true;
            
            EventBus.Send<IGameStartListener>(listener => listener.OnStartGame());
        }

        private void OnSpawned(Player player)
        {
            player.Died += OnDiedPlayer;
        }

        private void OnDiedPlayer()
        {
            if (_playerLife.TryDecrease())
            {
                _playerSpawner.SpawnPlayerWithDelay();
            }
            else
            {
                IsActivity = false;
                GameEnded?.Invoke();
            }
        }
    }
}