using UnityEngine;

public interface IArmyView
{
    void UpdateHealthVisual(float healthPercent);
    void PlayMoveAnimation(bool isMoving);
    void SetPosition(Vector3 position);
}