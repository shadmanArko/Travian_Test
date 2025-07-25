using UnityEngine;

namespace _Scripts.ObstacleSystem.Presenter
{
    public interface IObstaclePresenter
    {
        void Initialize();
        void Dispose();
        void SpawnAt(Vector3 position);
    }
}