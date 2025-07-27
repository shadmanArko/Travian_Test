using System;
using UnityEngine;

namespace _Scripts.ObstacleSystem.Presenter
{
    public interface IObstaclePresenter : IDisposable
    {
        void Initialize();
        void UpdateObstacle();
        void ActivateObstacle();
        void DeactivateObstacle();
        void ResetObstacle();
        void SetPosition(Vector3 position);
        Vector3 GetPosition();
        bool IsActive();
    }
}