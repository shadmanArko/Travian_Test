using _Scripts.Configs;
using _Scripts.ObstacleSystem.Model;
using _Scripts.ObstacleSystem.View;
using UnityEngine;

namespace _Scripts.ObstacleSystem.Presenter
{
    public class ObstaclePresenter : IObstaclePresenter
    {
        private readonly IObstacleModel _model;
        private readonly ObstacleView _view;
        private readonly GameConfig _config;
        private bool _disposed;

        public ObstaclePresenter(IObstacleModel model, ObstacleView view, GameConfig config)
        {
            _model = model;
            _view = view;
            _config = config;
            
            Initialize();
        }

        public void Initialize()
        {
            if (_disposed || _view == null) return;
            
            var spawnPosition = _config.obstacleSpawnPoint;
            spawnPosition.y = Random.Range(_config.minHeight, _config.maxHeight);
            SetPosition(spawnPosition);
            
            _model.SetActive(true);
            _model.UpdatePosition(spawnPosition);
            _model.Reset();
            
            DeactivateObstacle();
            
            if (_view.transform != null)
            {
                _view.transform.localScale = Vector3.one;
            }
        }

        public void UpdateObstacle()
        {
            var currentPosition = _view.transform.position;
            currentPosition.x -= _config.obstacleSpeed * Time.deltaTime;
            _view.transform.position = currentPosition;
            
            _model.UpdatePosition(currentPosition);
        }

        public void ActivateObstacle()
        {
            if (_disposed || _view == null || _view.gameObject == null) return;
            
            _view.gameObject.SetActive(true);
            _model.SetActive(true);
        }

        public void DeactivateObstacle()
        {
            if (_disposed || _view == null || _view.gameObject == null) return;
            
            _view.gameObject.SetActive(false);
            _model.SetActive(false);
        }

        public void ResetObstacle()
        {
            if (_disposed || _view == null) return;
            
            var spawnPosition = _config.obstacleSpawnPoint;
            spawnPosition.y = Random.Range(_config.minHeight, _config.maxHeight);
            SetPosition(spawnPosition);
            _model.Reset();
        }

        public void SetPosition(Vector3 position)
        {
            if (_disposed || _view == null || _view.transform == null) return;
            
            _view.transform.position = position;
            _model.UpdatePosition(position);
        }

        public Vector3 GetPosition()
        {
            if (_disposed || _view == null || _view.transform == null) 
                return Vector3.zero;
                
            return _view.transform.position;
        }

        public bool IsActive()
        {
            if (_disposed || _view == null || _view.gameObject == null) 
                return false;
                
            return _view.gameObject.activeInHierarchy && _model.IsActive();
        }

        public void Dispose()
        {
            if (_disposed) return;
            
            _disposed = true;
            
            if (_view != null && _view.gameObject != null)
            {
                if (Application.isPlaying)
                {
                    Object.Destroy(_view.gameObject);
                }
                else
                {
                    Object.DestroyImmediate(_view.gameObject);
                }
            }
        }
    }
}