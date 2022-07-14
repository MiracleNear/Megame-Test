using System;
using System.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace Spawner
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private Vector2 _position;
        [SerializeField] private int _spawnDelayAfterDeath;
        [SerializeField] private KeyboardInput _keyboardInput;

        private Action<Player> _spawned;
       
        public void Init(Action<Player> spawned)
		{
            _spawned = spawned;
		}

        private void SpawnPlayer()
        {
            Player player = Instantiate(_player, _position, quaternion.identity);

            player.BindInput(_keyboardInput);

            _spawned?.Invoke(player);
        }

        public void InitialSpawn()
		{
            SpawnPlayer();
		}

        public void SpawnPlayerWithDelay()
		{
            StartCoroutine(SpawnWithDelay());
		}

        private IEnumerator SpawnWithDelay()
        {
            yield return new WaitForSeconds(_spawnDelayAfterDeath);
            
            SpawnPlayer();
        }
    }
}