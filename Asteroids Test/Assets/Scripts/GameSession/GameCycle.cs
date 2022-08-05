using Spawner;
using UnityEngine;

namespace GameSession
{
    public class GameCycle : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private GameStarter _gameStarter;
        private void OnEnable()
        {
            _gameStarter.GameLaunched += OnGameLaunched;
            _playerSpawner.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            _gameStarter.GameLaunched -= OnGameLaunched;
            _playerSpawner.Spawned += OnSpawned;
        }

        private void OnGameLaunched()
        {
            _playerSpawner.InitialSpawn();
        }

        private void OnSpawned(Player player)
        {
            player.Died += OnDied;
        }

        private void OnDied()
        {
            if (_playerLife.TryDecrease())
            {
                _playerSpawner.SpawnPlayerWithDelay();
            }
            else
            {
                GameEnd();
            }
        }

        private void GameEnd()
        {
            
        }
    }
}
