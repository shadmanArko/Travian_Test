using UniRx;
using UnityEngine;

namespace _Scripts.ObstacleSystem.Model
{
    public interface IObstacleModel
    {
        IReadOnlyReactiveProperty<Vector3> Position { get; }
        IReadOnlyReactiveProperty<bool> IsActive { get; }
        void UpdatePosition(Vector3 position);
        void SetActive(bool active);
        void Reset(Vector3 startPosition);
    }
}