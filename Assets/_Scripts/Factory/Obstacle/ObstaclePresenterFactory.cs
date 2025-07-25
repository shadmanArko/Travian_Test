using _Scripts.Configs;
using _Scripts.ObstacleSystem.Model;
using _Scripts.ObstacleSystem.Presenter;
using _Scripts.ObstacleSystem.View;
using Zenject;

namespace _Scripts.Factory.Obstacle
{
    public class ObstaclePresenterFactory : PlaceholderFactory<ObstacleView, GameConfig, IObstaclePresenter>
    {
        public override IObstaclePresenter Create(ObstacleView view, GameConfig config)
        {
            var model = new ObstacleModel();
            return new ObstaclePresenter(model, view, config);
        }
    }
}