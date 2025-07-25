using System;
using UniRx;

public interface ITimer
{
    bool IsRunning { get; }
    bool IsCompleted { get; }
    bool IsPaused { get; }
    float RemainingTime { get; }
    float TotalTime { get; }
    float Progress { get; } // 0 to 1
    
    IObservable<Unit> OnComplete { get; }
    IObservable<float> OnTick { get; }
    IObservable<bool> OnPauseStateChanged { get; }
    
    void Start();
    void Pause();
    void Resume();
    void Stop();
    void Reset();
    void AddTime(float seconds);
}