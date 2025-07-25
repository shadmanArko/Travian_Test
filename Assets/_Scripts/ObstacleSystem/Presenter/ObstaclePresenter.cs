using System;
using _Scripts.Configs;
using _Scripts.ObstacleSystem.Model;
using _Scripts.ObstacleSystem.View;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Scripts.ObstacleSystem.Presenter
{
    public class ObstaclePresenter : IObstaclePresenter, IInitializable, IDisposable
    {
        private readonly IObstacleModel _model;
        private readonly ObstacleView _view;
        private readonly GameConfig _config;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
    
        public IObstacleModel Model => _model; 
    
        public ObstaclePresenter(IObstacleModel model, ObstacleView view, GameConfig config)
        {
            _model = model;
            _view = view;
            _config = config;
        }
    
        public void Initialize()
        {
            _model.Position.Subscribe(pos => _view.SetPosition(pos)).AddTo(_disposables);
            _model.IsActive.Subscribe(active => _view.SetActive(active)).AddTo(_disposables);
            
            Observable.EveryUpdate()
                .Where(_ => _model.IsActive.Value)
                .Subscribe(_ => 
                {
                    var currentPos = _view.GetPosition();
                    var newPos = currentPos + Vector3.left * _config.obstacleSpeed * Time.deltaTime;
                    _model.UpdatePosition(newPos);
                
                    // Deactivate if too far left
                    if (newPos.x < _config.despawnDistance)
                    {
                        _model.SetActive(false);
                    }
                })
                .AddTo(_disposables);
        }
    
        public void SpawnAt(Vector3 position)
        {
            _model.Reset(position);
        }
    
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}