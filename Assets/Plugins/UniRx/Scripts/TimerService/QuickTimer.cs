using System;

public static class QuickTimer
{
    private static TimerService globalTimerService = new TimerService();
    
    // For quick one-off timers
    public static ITimer After(float seconds, Action callback)
    {
        return globalTimerService.CreateTimer(seconds)
            .OnCompleted(callback)
            .StartImmediately();
    }
    
    public static ITimer Every(float interval, Action callback)
    {
        return globalTimerService.CreateRepeatingTimer(interval)
            .OnCompleted(callback)
            .StartImmediately();
    }
    
    public static ITimer Countdown(float duration, Action<float> onTick, Action onComplete)
    {
        return globalTimerService.CreateTimer(duration)
            .OnTick(onTick)
            .OnCompleted(onComplete)
            .StartImmediately();
    }
}