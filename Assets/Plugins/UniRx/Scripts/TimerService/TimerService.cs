using System;
using UniRx;

public class TimerService : ITimerService, IDisposable
{
    private readonly CompositeDisposable disposables = new CompositeDisposable();
    private readonly ReactiveCollection<ITimer> activeTimers = new ReactiveCollection<ITimer>();
    
    public TimerService()
    {
        // Auto-cleanup completed timers
        activeTimers.ObserveAdd()
            .Subscribe(addEvent => 
            {
                var timer = addEvent.Value;
                timer.OnComplete
                    .Take(1)
                    .Subscribe(_ => 
                    {
                        if (!((ReactiveTimer)timer).IsRunning)
                        {
                            activeTimers.Remove(timer);
                        }
                    })
                    .AddTo(disposables);
            })
            .AddTo(disposables);
    }
    
    public ITimer CreateTimer(float duration)
    {
        var timer = new ReactiveTimer(duration, false);
        activeTimers.Add(timer);
        return timer;
    }
    
    public ITimer CreateRepeatingTimer(float interval)
    {
        var timer = new ReactiveTimer(interval, true);
        activeTimers.Add(timer);
        return timer;
    }
    
    public ITimer CreateCountdownTimer(float duration)
    {
        return CreateTimer(duration); // Same as regular timer
    }
    
    public void PauseAllTimers()
    {
        foreach (var timer in activeTimers)
        {
            timer.Pause();
        }
    }
    
    public void ResumeAllTimers()
    {
        foreach (var timer in activeTimers)
        {
            timer.Resume();
        }
    }
    
    public void StopAllTimers()
    {
        foreach (var timer in activeTimers)
        {
            timer.Stop();
        }
        activeTimers.Clear();
    }
    
    public void Dispose()
    {
        StopAllTimers();
        disposables?.Dispose();
    }
}