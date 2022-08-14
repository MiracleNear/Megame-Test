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

        public event Action<Player> Spawned;
        
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

        private void SpawnPlayer()
        {
            Player player = Instantiate(_player, _position, quaternion.identity);
            
            Spawned?.Invoke(player);
        }
    }
}