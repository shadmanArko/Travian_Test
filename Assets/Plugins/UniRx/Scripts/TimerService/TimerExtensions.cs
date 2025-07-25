using System;
using UniRx;

public static class TimerExtensions
{
    public static ITimer OnCompleted(this ITimer timer, Action callback)
    {
        timer.OnComplete
            .Subscribe(_ => callback())
            .AddTo(GetDisposable(timer));
        return timer;
    }

    public static ITimer OnTick(this ITimer timer, Action<float> callback)
    {
        timer.OnTick
            .Subscribe(callback)
            .AddTo(GetDisposable(timer));
        return timer;
    }

    public static ITimer OnProgress(this ITimer timer, Action<float> callback)
    {
        timer.OnTick
            .Subscribe(_ => callback(timer.Progress))
            .AddTo(GetDisposable(timer));
        return timer;
    }

    public static ITimer StartImmediately(this ITimer timer)
    {
        timer.Start();
        return timer;
    }

    public static IObservable<float> AsObservable(this ITimer timer)
    {
        return timer.OnTick
            .TakeWhile(_ => timer.IsRunning);
    }

    // Helper to safely get the CompositeDisposable
    private static CompositeDisposable GetDisposable(ITimer timer)
    {
        if (timer is ReactiveTimer reactiveTimer)
        {
            return reactiveTimer.Disposables;
        }

        throw new InvalidOperationException("Timer must be of type ReactiveTimer to use AddTo().");
    }
}