using UniRx;

namespace _Scripts.ScoreSystem.Model
{
    public class ScoreModel : IScoreModel
    {
        private readonly ReactiveProperty<int> _currentScore = new ReactiveProperty<int>(0);
        private float _timeAccumulator = 0f;
    
        public IReadOnlyReactiveProperty<int> CurrentScore => _currentScore;
    
        public void UpdateScore(float deltaTime)
        {
            _timeAccumulator += deltaTime;
            _currentScore.Value = (int)(_timeAccumulator * 10f);
        }
    
        public void ResetScore()
        {
            _currentScore.Value = 0;
            _timeAccumulator = 0f;
        }
    }
}