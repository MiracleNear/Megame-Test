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

        private Ufo _activeUfo;
        private Player _target;
        private Coroutine _spawnWithDelay;
        private bool _isPaused => PauseManager.GetInstance().IsPaused;
        private bool _isTargetNotNull => _target != null;

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
            Spawn();
        }
        
        private void OnSpawned(Player player)
        {
            player.Died += OnDiedPlayer;
            _target = player;
            Spawn();
        }

        private void OnDiedPlayer()
        {
            if (_activeUfo)
            {
                _activeUfo.Kill();
            }
            
            StopCoroutine(_spawnWithDelay);
        }

        private void Spawn()
        {
            _spawnWithDelay = StartCoroutine(SpawnWithDelay());
        }
        
        private IEnumerator SpawnWithDelay()
        {
            yield return new WaitUntil(() => _isTargetNotNull);
            
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

            _activeUfo = Create(EnemyType.Ufo, EnemyPlacer.CreateUfoPlacer(EnemyCollider, _offsetFromTheVerticalBorderInPercent));
            _activeUfo.SetTarget(_target.transform);
        }
    }
}