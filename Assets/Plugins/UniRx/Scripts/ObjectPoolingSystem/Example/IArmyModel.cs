using UniRx;
using UnityEngine;

public interface IArmyModel : IPoolableModel
{
    IReadOnlyReactiveProperty<float> Health { get; }
    IReadOnlyReactiveProperty<Vector3> Position { get; }
    IReadOnlyReactiveProperty<bool> IsMoving { get; }
    
    void TakeDamage(float damage);
    void MoveTo(Vector3 targetPosition);
}