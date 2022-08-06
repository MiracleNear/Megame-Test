using UnityEngine;

namespace Score
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreCounter : MonoBehaviour
    {
        private ScoreView _scoreView;
        private int _points;

        private void Awake()
        {
            _scoreView = GetComponent<ScoreView>();
            
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