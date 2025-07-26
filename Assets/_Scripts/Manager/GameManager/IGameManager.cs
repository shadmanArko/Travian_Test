namespace _Scripts.Manager.GameManager
{
    using System;

    public interface IGameManager : IDisposable
    {
        void StartGame();
        void EndGame();
        void PauseGame();
        void ResumeGame();
        void UpdateGame();
    }
}