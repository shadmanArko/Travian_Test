using _Scripts.ObstacleSystem.Presenter;

namespace _Scripts.Factory.Obstacle
{
    public interface IObstacleFactory
    {
        IObstaclePresenter CreateObstacle();
        void ReturnObstacle(IObstaclePresenter obstacle);
    }
}
