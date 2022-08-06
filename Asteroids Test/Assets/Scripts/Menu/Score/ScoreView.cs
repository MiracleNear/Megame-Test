using UnityEngine;
using UnityEngine.UI;

namespace Score
{
    [RequireComponent(typeof(Text))]
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private string _format;
        
        private Text _pointsView;

        private void Awake()
        {
            _pointsView = GetComponent<Text>();
        }

        public void UpdateScore(int points)
        {
            _pointsView.text = string.Format(_format, points);
        }
    }
}