using UnityEngine;

namespace DefaultNamespace.Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private GameStarter _gameStarter;
        
        private ScoreView _scoreView;
        private int _points;

        private void Awake()
        {
            _scoreView = GetComponent<ScoreView>();
            
            _gameStarter.GameLaunched += OnGameLaunched;
            
            gameObject.SetActive(false);
        }
        

        private void OnGameLaunched()
        {
            gameObject.SetActive(true);
            
            _scoreView.UpdateScore(_points);
        }


        public void Increase(int points)
        {
            _points += points;
            _scoreView.UpdateScore(_points);
        }
    }
}