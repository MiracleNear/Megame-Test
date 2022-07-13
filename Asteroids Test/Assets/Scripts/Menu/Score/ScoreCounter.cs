using Enemies;
using Spawner;
using UnityEngine;

namespace DefaultNamespace.Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private EnemySpawner _enemySpawner;

        private ScoreView _scoreView;
        private int _points;

        private void Awake()
        {
            _scoreView = GetComponent<ScoreView>();
        }

        private void Start()
        {
            _scoreView.UpdateScore(_points);
        }

        private void OnEnable()
        {
            _enemySpawner.Spawned += OnSpawned;
        }

        private void OnDisable()
        {
            _enemySpawner.Spawned += OnSpawned;
        }

        private void OnSpawned(Enemy enemy)
        {
            enemy.Destroyed += OnDestroyed;
            enemy.DestroyedByPlayer += OnDestroyedByPlayer;
        }

        private void OnDestroyed(Enemy enemy)
        {
            enemy.Destroyed -= OnDestroyed;
        }

        private void OnDestroyedByPlayer(int points)
        {
            _points += points;
            _scoreView.UpdateScore(_points);
        }
    }
}