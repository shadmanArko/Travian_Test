using UniRx;
using UnityEngine;

namespace _Scripts.ObstacleSystem.Model
{
    public class ObstacleModel : IObstacleModel
    {
        private readonly ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);
    
        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public IReadOnlyReactiveProperty<bool> IsActive => _isActive;
    
        public void UpdatePosition(Vector3 position)
        {
            _position.Value = position;
        }
    
        public void SetActive(bool active)
        {
            _isActive.Value = active;
        }
    
        public void Reset(Vector3 startPosition)
        {
            _position.Value = startPosition;
            _isActive.Value = true;
        }
    }
}