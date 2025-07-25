using UnityEngine;
using Zenject;

public class CameraDragHandler : IInitializable, ITickable
{
    private readonly Camera _camera;
    private Vector3 _lastTouchWorldPos;
    private bool _isDragging;

    public CameraDragHandler(Camera camera)
    {
        _camera = camera;
    }

    public void Initialize() { }

    public void Tick()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _isDragging = true;
            _lastTouchWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0) && _isDragging)
        {
            Vector3 currentWorldPos = _camera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 delta = _lastTouchWorldPos - currentWorldPos;
            _camera.transform.position += delta;
            _lastTouchWorldPos = currentWorldPos;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _isDragging = false;
        }
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 1)
        {
            var touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                _isDragging = true;
                _lastTouchWorldPos = _camera.ScreenToWorldPoint(touch.position);
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                Vector3 currentWorldPos = _camera.ScreenToWorldPoint(touch.position);
                Vector3 delta = _lastTouchWorldPos - currentWorldPos;
                _camera.transform.position += delta;
                _lastTouchWorldPos = currentWorldPos;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                _isDragging = false;
            }
        }
    }
}

/*
 * Letter addition
 * Clamp camera position
   Add momentum or damping with DOTween or velocity buffer
   Ignore drag when touching UI (add a UIBlockerService)
*/