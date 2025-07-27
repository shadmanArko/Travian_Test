using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.ScoreSystem.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
    
        public void UpdateScoreDisplay(int score)
        {
            scoreText.text = $"Score: {score}";
        }
    }
}