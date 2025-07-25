using DG.Tweening;
using UniRx;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(Collider2D))]
public class ReactiveButton : MonoBehaviour
{
    [Inject] private ReactiveInputHandler _inputHandler;
    private Collider2D _collider;

    private void Start()
    {
        _collider = GetComponent<Collider2D>();

        _inputHandler.OnTouch
            .Where(pos => _collider.OverlapPoint(pos))
            .Subscribe(_ => OnButtonPressed())
            .AddTo(this);
    }

    private void OnButtonPressed()
    {
        transform.DOScale(1.2f, 0.1f)
            .SetLoops(2, LoopType.Yoyo);
    }
}