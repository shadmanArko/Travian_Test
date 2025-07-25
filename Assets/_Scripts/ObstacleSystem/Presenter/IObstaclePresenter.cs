using _Scripts.ObstacleSystem.Model;
using UnityEngine;

namespace _Scripts.ObstacleSystem.Presenter
{
    public interface IObstaclePresenter
    {
        IObstacleModel Model { get; }
        void Initialize();
        void Dispose();
        void SpawnAt(Vector3 position);
    }
}