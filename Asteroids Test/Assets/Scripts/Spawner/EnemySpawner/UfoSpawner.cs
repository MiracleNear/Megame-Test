using System.Collections;
using Enemies;
using Factories;
using UnityEngine;

namespace Spawner
{
    [RequireComponent(typeof(UfoPlacerFactory))]
    public class UfoSpawner : EnemySpawner<Ufo>
    {
        [SerializeField] private float _minAppearanceTime, _maxAppearanceTime;
        [SerializeField] private PlayerSpawner _playerSpawner;

        private Ufo _activeUfo;
        private Player _target;
        private Coroutine _spawnWithDelay;

        protected override void SubScribe()
        {
            _playerSpawner.Spawned += OnSpawned;
            
            base.SubScribe();
        }

        protected override void UnSubscribe()
        {
            _playerSpawner.Spawned -= OnSpawned;
            
            base.UnSubscribe();
        }

        protected override void OnDestroyEnemy(Ufo enemy)
        {
            SpawnUfo();
        }

        private void OnSpawned(Player player)
        {
            _target = player;
            player.Died += OnDiedPlayer;
            
            SpawnUfo();
        }

        private void OnDiedPlayer()
        {
            if (_activeUfo != null)
            {
                _activeUfo.Kill();
            }
            else
            {
                ClearSpawn();
            }
        }

        private void SpawnUfo()
        {
            ClearSpawn();
            
            _spawnWithDelay = StartCoroutine(SpawnWithDelay());
        }

        private void ClearSpawn()
        {
            if (_spawnWithDelay != null)
            {
                StopCoroutine(_spawnWithDelay);

                _spawnWithDelay = null;
            }
        }

        private IEnumerator SpawnWithDelay()
        {
            float appearanceTime = Random.Range(_minAppearanceTime, _maxAppearanceTime);
                                    
            yield return new WaitForSeconds(appearanceTime);

            _activeUfo = Create(EnemyType.Ufo, EnemyPlacer);
            
            _activeUfo.Init(_target.transform);
        }
    }
}