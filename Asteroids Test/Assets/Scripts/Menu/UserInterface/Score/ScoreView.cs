using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(Text))]
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private string _format;
        [SerializeField] private Text _pointsView;
        
        public void UpdateScore(int points)
        {
            _pointsView.text = string.Format(_format, points);
        }
    }
}