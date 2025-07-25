using System.Collections.Generic;
using _Scripts.Configs;
using _Scripts.EventBus;
using _Scripts.EventBus.GameEvents;
using _Scripts.Factory.Obstacle;
using _Scripts.ObstacleSystem.Presenter;
using _Scripts.ObstacleSystem.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.Manager.GameManager
{
    public class GameManager : IGameManager
    {
        private readonly IEventBus _eventBus;
        private readonly GameConfig _gameConfig;
        private readonly ObstaclePresenterFactory _obstacleFactory;
        private readonly DiContainer _container;
        
        private List<IObstaclePresenter> _obstaclePool = new List<IObstaclePresenter>();
        private CompositeDisposable _disposables = new CompositeDisposable();
        private bool _isGameRunning = false;
        
        public GameManager(
            IEventBus eventBus,
            GameConfig gameConfig, 
            ObstaclePresenterFactory obstacleFactory, 
            DiContainer container)
        {
            _eventBus = eventBus;
            _gameConfig = gameConfig;
            _obstacleFactory = obstacleFactory;
            _container = container;

            InitializeObstaclePool();
            SubscribeToEvents();
            StartGame();
        }

        private void InitializeObstaclePool()
        {
            for (int i = 0; i < 5; i++) // Pool of 5 obstacles
            {
                var view = _container.Instantiate<ObstacleView>();

                var presenter = _obstacleFactory.Create(view, _gameConfig);
                presenter.Initialize();
                _obstaclePool.Add(presenter);
                view.SetActive(false);
            }
        }

        private void SubscribeToEvents()
        {
            _eventBus.OnEvent<GameOverEvent>()
                .Subscribe(_ => _isGameRunning = false)
                .AddTo(_disposables);

            // Obstacle spawning
            Observable.Interval(System.TimeSpan.FromSeconds(_gameConfig.spawnInterval))
                .Where(_ => _isGameRunning)
                .Subscribe(_ => SpawnObstacle())
                .AddTo(_disposables);
        }

        private void SpawnObstacle()
        {
            var availableObstacle = _obstaclePool.Find(o => !o.Model.IsActive.Value);
            if (availableObstacle != null)
            {
                float randomHeight = Random.Range(_gameConfig.minHeight, _gameConfig.maxHeight);
                Vector3 spawnPos = new Vector3(_gameConfig.obstacleSpawnPoint.x, randomHeight, 0);
                availableObstacle.SpawnAt(spawnPos);
            }
        }

        public void StartGame()
        {
            _isGameRunning = true;
            _eventBus.Publish(new GameStartEvent());
        }

        public void RestartGame()
        {
            foreach (var obstacle in _obstaclePool)
            {
                obstacle.Model.SetActive(false);
            }

            StartGame();
        }

        private void OnDestroy()
        {
            _disposables.Dispose();
            foreach (var obstacle in _obstaclePool)
            {
                obstacle.Dispose();
            }
        }
    }
}