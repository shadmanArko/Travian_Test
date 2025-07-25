using _Scripts.Configs;
using _Scripts.EventBus;
using _Scripts.PlayerSystem.Model;
using _Scripts.PlayerSystem.View;
using UniRx;
using UnityEngine;

namespace _Scripts.PlayerSystem.Presenter
{
    public class PlayerPresenter : IPlayerPresenter
    {
        private readonly IPlayerModel _model;
        private readonly GameConfig _config;
        private readonly IEventBus _eventBus;
        private readonly CompositeDisposable _disposable = new CompositeDisposable();

        private PlayerView _view;

        public PlayerPresenter(IPlayerModel model, GameConfig config, IEventBus eventBus, PlayerView view)
        {
            _model = model;
            _config = config;
            _eventBus = eventBus;
            _view = view;
        }

        public void Initialize()
        {
            _view.SetGravity(_config.gravity);

            _view.OnJumpInput += HandleJumpInput;
            _view.OnCollisionDetected += HandleCollision;

            Observable.EveryUpdate()
                .Where(_ => _model.IsAlive.Value)
                .Subscribe(_ =>
                    {
                        _model.UpdatePosition(_view.GetPosition());
                        _model.SetGrounded(_view.IsGroundedCheck());
                    }
                ).AddTo(_disposable);
        }

        private void HandleCollision(Collider2D obj)
        {
            throw new System.NotImplementedException();
        }

        private void HandleJumpInput()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}