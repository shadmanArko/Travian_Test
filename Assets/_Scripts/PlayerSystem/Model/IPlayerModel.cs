using UniRx;
using UnityEngine;

namespace _Scripts.PlayerSystem.Model
{
    public interface IPlayerModel
    {
        IReadOnlyReactiveProperty<Vector3> Position { get; }
        IReadOnlyReactiveProperty<bool> IsGrounded { get; }
        IReadOnlyReactiveProperty<bool> IsAlive { get; }

        void Jump();
        void UpdatePosition(Vector3 position);
        void SetGrounded(bool grounded);
        void Die();
        void ReSet();
    }
}