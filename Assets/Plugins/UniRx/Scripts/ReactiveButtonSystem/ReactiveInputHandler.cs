using System;
using UniRx;
using UnityEngine;
using Zenject;

public class ReactiveInputHandler : IInitializable, ITickable, IDisposable
{
    private readonly Camera _camera;
    private readonly Subject<Vector2> _touchSubject = new();
    public IObservable<Vector2> OnTouch => _touchSubject;

    public ReactiveInputHandler(Camera camera)
    {
        _camera = camera;
    }

    public void Initialize() { }

    public void Tick()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        if (Input.GetMouseButtonDown(0))
        {
            var worldPos = (Vector2)_camera.ScreenToWorldPoint(Input.mousePosition);
            _touchSubject.OnNext(worldPos);
        }
#else
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            var worldPos = (Vector2)_camera.ScreenToWorldPoint(Input.GetTouch(0).position);
            _touchSubject.OnNext(worldPos);
        }
#endif
    }

    public void Dispose()
    {
        _touchSubject?.Dispose();
    }
}