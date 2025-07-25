using UniRx;

namespace _Scripts.ScoreSystem.Model
{
    public interface IScoreModel
    {
        IReadOnlyReactiveProperty<int> CurrentScore { get; }
        void UpdateScore(float deltaTime);
        void ResetScore();
    }
}