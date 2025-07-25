using System;
using UniRx;
using UnityEngine;

public class ReactiveTimer : ITimer, IDisposable
{
    private readonly Subject<Unit> onCompleteSubject = new Subject<Unit>();
    private readonly Subject<float> onTickSubject = new Subject<float>();
    private readonly Subject<bool> onPauseStateChangedSubject = new Subject<bool>();

    private readonly CompositeDisposable disposables = new CompositeDisposable();
    private IDisposable timerDisposable;

    private float totalTime;
    private float currentTime;
    private bool isRunning;
    private bool isPaused;
    private bool isCompleted;
    private bool isRepeating;

    public bool IsRunning => isRunning;
    public bool IsCompleted => isCompleted;
    public bool IsPaused => isPaused;
    public float RemainingTime => Mathf.Max(0, totalTime - currentTime);
    public float TotalTime => totalTime;
    public float Progress => totalTime > 0 ? Mathf.Clamp01(currentTime / totalTime) : 0;

    public IObservable<Unit> OnComplete => onCompleteSubject.AsObservable();
    public IObservable<float> OnTick => onTickSubject.AsObservable();
    public IObservable<bool> OnPauseStateChanged => onPauseStateChangedSubject.AsObservable();

    // Expose disposables for extensions
    public CompositeDisposable Disposables => disposables;

    public ReactiveTimer(float duration, bool repeating = false)
    {
        totalTime = duration;
        currentTime = 0f;
        isRepeating = repeating;

        // Track Subjects for disposal
        disposables.Add(onCompleteSubject);
        disposables.Add(onTickSubject);
        disposables.Add(onPauseStateChangedSubject);
    }

    public void Start()
    {
        if (isRunning) return;

        if (!isRepeating && isCompleted)
        {
            Reset();
        }

        isRunning = true;
        isPaused = false;
        isCompleted = false;

        timerDisposable?.Dispose();
        timerDisposable = Observable.EveryUpdate()
            .Where(_ => !isPaused)
            .Subscribe(_ => UpdateTimer())
            .AddTo(disposables);
    }

    public void Pause()
    {
        if (!isRunning || isPaused) return;

        isPaused = true;
        onPauseStateChangedSubject.OnNext(true);
    }

    public void Resume()
    {
        if (!isRunning || !isPaused) return;

        isPaused = false;
        onPauseStateChangedSubject.OnNext(false);
    }

    public void Stop()
    {
        if (!isRunning) return;

        isRunning = false;
        isPaused = false;
        timerDisposable?.Dispose();
    }

    public void Reset()
    {
        Stop();
        currentTime = 0f;
        isCompleted = false;
    }

    public void AddTime(float seconds)
    {
        totalTime += seconds;
    }

    private void UpdateTimer()
    {
        currentTime += Time.deltaTime;
        onTickSubject.OnNext(RemainingTime); // Or use Progress if that's what you expect

        if (currentTime >= totalTime)
        {
            onCompleteSubject.OnNext(Unit.Default);

            if (isRepeating)
            {
                currentTime = 0f;
            }
            else
            {
                isCompleted = true;
                isRunning = false;
                timerDisposable?.Dispose();
            }
        }
    }

    public void Dispose()
    {
        timerDisposable?.Dispose();
        disposables?.Dispose();
    }
}
