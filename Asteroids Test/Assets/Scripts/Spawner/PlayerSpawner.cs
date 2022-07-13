using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        public event Action<Player> Spawned; 
        
        [SerializeField] private Player _player;
        [SerializeField] private PlayerLife _playerLife;
        [SerializeField] private Vector2 _position;
        [SerializeField] private float _spawnDelayAfterDeath;
        [SerializeField] private KeyboardInput _keyboardInput;

        private bool _isSpawnPossibility = true;
        
        private void Start()
        {
            SpawnPlayer();
        }

        private void OnEnable()
        {
            _playerLife.Decreased += OnDecreasedLife;
        }

        private void OnDisable()
        {
            _playerLife.Decreased -= OnDecreasedLife;
        }

        private void SpawnPlayer()
        {
            Player player = Instantiate(_player, _position, quaternion.identity);

            player.BindInput(_keyboardInput);
            player.Init(_playerLife);
            Spawned?.Invoke(player);
            player.Died += OnDied;
        }

        private void OnDied(Player player)
        {
            player.Died -= OnDied;
            
            if (_isSpawnPossibility)
            {
                StartCoroutine(SpawnWithDelay(_spawnDelayAfterDeath));
            }
        }

        private void OnDecreasedLife(int amount)
        {
            if (amount < 0)
            {
                _isSpawnPossibility = false;
            }
        }

        private IEnumerator SpawnWithDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            SpawnPlayer();
        }
    }
}