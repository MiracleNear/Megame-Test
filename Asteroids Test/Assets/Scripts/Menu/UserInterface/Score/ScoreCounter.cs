using UnityEngine;

namespace Menu
{
    [RequireComponent(typeof(ScoreView))]
    public class ScoreCounter : UserInterfaceElement
    {
        private ScoreView _scoreView;
        private int _points;
        
        public override void Init()
        {
            _scoreView = GetComponent<ScoreView>();
        }

        public override void OnStartGame()
        {
            _scoreView.UpdateScore(0);
        }

        public void Increase(int points)
        {
            _points += points;
            _scoreView.UpdateScore(_points);
        }
    }
}