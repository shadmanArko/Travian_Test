using _Scripts.Configs;
using _Scripts.ObstacleSystem.Model;
using _Scripts.ObstacleSystem.Presenter;
using _Scripts.ObstacleSystem.View;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Scripts.Factory.Obstacle
{
    public class ObstacleFactory : IObstacleFactory
    {
        private readonly DiContainer _container;
        private readonly ObstacleView _obstaclePrefab;
        private readonly GameConfig _gameConfig;
        private readonly Queue<IObstaclePresenter> _obstaclePool = new Queue<IObstaclePresenter>();

        public ObstacleFactory(DiContainer container, ObstacleView obstaclePrefab, GameConfig gameConfig)
        {
            _container = container;
            _obstaclePrefab = obstaclePrefab;
            _gameConfig = gameConfig;
            
            InitializePool();
        }

        private void InitializePool()
        {
            for (int i = 0; i < 5; i++)
            {
                var presenter = CreateNewObstacle();
                presenter.DeactivateObstacle();
                _obstaclePool.Enqueue(presenter);
            }
        }

        public IObstaclePresenter CreateObstacle()
        {
            IObstaclePresenter presenter;
            
            if (_obstaclePool.Count > 0)
            {
                presenter = _obstaclePool.Dequeue();
                presenter.ResetObstacle();
            }
            else
            {
                presenter = CreateNewObstacle();
            }
            
            presenter.ActivateObstacle();
            return presenter;
        }

        public void ReturnObstacle(IObstaclePresenter obstacle)
        {
            obstacle.DeactivateObstacle();
            _obstaclePool.Enqueue(obstacle);
        }

        private IObstaclePresenter CreateNewObstacle()
        {
            var obstacleView = _container.InstantiatePrefabForComponent<ObstacleView>(_obstaclePrefab);
            var obstacleModel = _container.Instantiate<ObstacleModel>();
            
            return _container.Instantiate<ObstaclePresenter>(new object[] { obstacleModel, obstacleView, _gameConfig });
        }
    }
}
