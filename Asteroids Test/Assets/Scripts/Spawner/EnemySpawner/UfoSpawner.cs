using System.Collections;
using Enemies;
using Factories;
using GameSession;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class UfoSpawner : EnemySpawner<Ufo>
    {
        [SerializeField] private float _minAppearanceTime, _maxAppearanceTime, _offsetFromTheVerticalBorderInPercent;
        [SerializeField] private PlayerSpawner _playerSpawner;
        [SerializeField] private Collider2D _ufoCollider;

        private Ufo _activeUfo;
        private Player _target;
        private Coroutine _spawnWithDelay;
        private bool _isPaused => PauseManager.GetInstance().IsPaused;
        
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
        
        protected override void OnReclaimed(Ufo enemy)
        {
            if (_target != null)
            {
                StartSpawn();
            }
        }
        
        private void OnSpawned(Player player)
        {
            player.Died += OnDiedPlayer;
            _target = player;
            StartSpawn();
        }

        private void OnDiedPlayer()
        {
            if (_activeUfo != null)
            {
                _activeUfo.Kill();
            }
            
            StopCoroutine(_spawnWithDelay);
        }

        private void StartSpawn()
        {
            _spawnWithDelay = StartCoroutine(SpawnWithDelay());
        }
        
        private IEnumerator SpawnWithDelay()
        {
            float duration = 0f;
            
            float delay = Random.Range(_minAppearanceTime, _maxAppearanceTime);
            
            while (duration < delay)
            {
                if (!_isPaused)
                {
                    duration += Time.deltaTime;
                }

                yield return null;
            }

            _activeUfo = Create(EnemyType.Ufo, new UfoPlacer(_ufoCollider, _offsetFromTheVerticalBorderInPercent));
            _activeUfo.Init(_target.transform);
        }
    }
}