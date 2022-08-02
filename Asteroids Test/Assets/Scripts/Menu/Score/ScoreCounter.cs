using Enemies;
using UnityEngine;

namespace DefaultNamespace.Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private EnemyPublisher _enemyPublisher;
        [SerializeField] private GameStarter _gameStarter;
        
        private ScoreView _scoreView;
        private int _points;

        private void Awake()
        {
            _scoreView = GetComponent<ScoreView>();
            
            _gameStarter.GameLaunched += OnGameLaunched;
            _enemyPublisher.Spawned += OnSpawned;
            _enemyPublisher.Removed += OnRemoved;
            
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _enemyPublisher.Spawned -= OnSpawned;
            _enemyPublisher.Removed -= OnRemoved;
        }

        private void OnGameLaunched()
        {
            gameObject.SetActive(true);
            
            _scoreView.UpdateScore(_points);
        }

        private void OnRemoved(Enemy enemy)
        {
            enemy.DestroyedByPlayer -= OnDestroyedByPlayer;
        }

        private void OnSpawned(Enemy enemy)
        {
            enemy.DestroyedByPlayer += OnDestroyedByPlayer;
        }

        private void OnDestroyedByPlayer(int points)
        {
            _points += points;
            _scoreView.UpdateScore(_points);
        }
    }
}