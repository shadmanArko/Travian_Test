using System;
using _Scripts.EventBus;
using _Scripts.EventBus.GameEvents;
using _Scripts.ScoreSystem.Model;
using _Scripts.ScoreSystem.View;
using UniRx;
using UnityEngine;
using Zenject;
using Object = System.Object;

namespace _Scripts.ScoreSystem.Presenter
{
    public class ScorePresenter : IScorePresenter, IInitializable, IDisposable
    {
        private readonly IScoreModel _model;
        private readonly IEventBus _eventBus;
        private readonly CompositeDisposable _disposables = new CompositeDisposable();
        private bool _isGameRunning = false;
    
        private ScoreView _view;
    
        public ScorePresenter(IScoreModel model, IEventBus eventBus, ScoreView view)
        {
            _model = model;
            _eventBus = eventBus;
            _view = view;
        }
    
        public void Initialize()
        {
            _model.CurrentScore.Subscribe(score => 
            {
                _view.UpdateScoreDisplay(score);
                _eventBus.Publish(new ScoreUpdateEvent { Score = score });
            }).AddTo(_disposables);
            
            _eventBus.OnEvent<GameStartEvent>()
                .Subscribe(_ => 
                {
                    _model.ResetScore();
                    _isGameRunning = true;
                })
                .AddTo(_disposables);
            
            _eventBus.OnEvent<GameOverEvent>()
                .Subscribe(_ => _isGameRunning = false)
                .AddTo(_disposables);
            
            Observable.EveryUpdate()
                .Where(_ => _isGameRunning)
                .Subscribe(_ => _model.UpdateScore(Time.deltaTime))
                .AddTo(_disposables);
        }
    
        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}