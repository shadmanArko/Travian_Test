using UnityEngine;

namespace _Scripts.ObstacleSystem.Model
{
    public interface IObstacleModel
    {
        Vector3 Position { get; }
        bool IsActive();
        void SetActive(bool active);
        void UpdatePosition(Vector3 position);
        void Reset();
    }
}