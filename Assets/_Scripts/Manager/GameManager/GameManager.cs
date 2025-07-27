using _Scripts.Configs;
using _Scripts.EventBus;
using _Scripts.EventBus.GameEvents;
using _Scripts.Factory.Obstacle;
using _Scripts.ObstacleSystem.Presenter;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace _Scripts.Manager.GameManager
{
    public class GameManager : IGameManager
    {
        private readonly IEventBus _eventBus;
        private readonly GameConfig _gameConfig;
        private readonly IObstacleFactory _obstacleFactory;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private readonly List<IObstaclePresenter> _activeObstacles = new List<IObstaclePresenter>();
        private float _lastSpawnTime;
        private bool _isGameRunning;

        public GameManager(IEventBus eventBus, GameConfig gameConfig, IObstacleFactory obstacleFactory)
        {
            _eventBus = eventBus;
            _gameConfig = gameConfig;
            _obstacleFactory = obstacleFactory;

            SubscribeToEvents();
            
            _eventBus.Publish(new GameStartEvent());
        }

        private void SubscribeToEvents()
        {
            _eventBus.OnEvent<GameStartEvent>()
                .Subscribe(_ => StartGame())
                .AddTo(_disposables);

            _eventBus.OnEvent<GameOverEvent>()
                .Subscribe(_ => EndGame())
                .AddTo(_disposables);

            Observable.EveryUpdate()
                .Where(_ => _isGameRunning)
                .Subscribe(_ => UpdateGame())
                .AddTo(_disposables);
        }

        public void StartGame()
        {
            _isGameRunning = true;
            _lastSpawnTime = Time.time;
            ClearAllObstacles();
        }

        public void EndGame()
        {
            _isGameRunning = false;
            ClearAllObstacles();
        }

        public void PauseGame()
        {
            _isGameRunning = false;
        }

        public void ResumeGame()
        {
            _isGameRunning = true;
        }

        public void UpdateGame()
        {
            UpdateObstacles();
            SpawnObstacleIfNeeded();
            RemoveInactiveObstacles();
        }

        private void UpdateObstacles()
        {
            foreach (var obstacle in _activeObstacles.ToList())
            {
                obstacle.UpdateObstacle();
                
                if (obstacle.GetPosition().x < _gameConfig.despawnDistance)
                {
                    _obstacleFactory.ReturnObstacle(obstacle);
                    _activeObstacles.Remove(obstacle);
                }
            }
        }

        private void SpawnObstacleIfNeeded()
        {
            if (Time.time - _lastSpawnTime >= _gameConfig.spawnInterval)
            {
                var obstacle = _obstacleFactory.CreateObstacle();
                obstacle.SetPosition(_gameConfig.obstacleSpawnPoint);
                _activeObstacles.Add(obstacle);
                _lastSpawnTime = Time.time;
            }
        }

        private void RemoveInactiveObstacles()
        {
            var inactiveObstacles = _activeObstacles.Where(o => !o.IsActive()).ToList();
            foreach (var obstacle in inactiveObstacles)
            {
                _obstacleFactory.ReturnObstacle(obstacle);
                _activeObstacles.Remove(obstacle);
            }
        }

        private void ClearAllObstacles()
        {
            foreach (var obstacle in _activeObstacles.ToList())
            {
                _obstacleFactory.ReturnObstacle(obstacle);
            }
            _activeObstacles.Clear();
        }

        public void Dispose()
        {
            ClearAllObstacles();
            _disposables?.Dispose();
        }
    }
}