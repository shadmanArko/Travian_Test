using UnityEngine;

namespace _Scripts.ObstacleSystem.Model
{
    public class ObstacleModel : IObstacleModel
    {
        private Vector3 _position;
        private bool _isActive;

        public Vector3 Position => _position;

        public bool IsActive()
        {
            return _isActive;
        }

        public void SetActive(bool active)
        {
            _isActive = active;
        }

        public void UpdatePosition(Vector3 position)
        {
            _position = position;
        }

        public void Reset()
        {
            _isActive = false;
            _position = Vector3.zero;
        }
    }
}