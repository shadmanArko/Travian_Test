using System;
using UniRx;
using UnityEngine;

namespace _Scripts.PlayerSystem.Model
{
    public class PlayerModel : IPlayerModel
    {
        private readonly ReactiveProperty<Vector3> _position = new ReactiveProperty<Vector3>();
        private readonly ReactiveProperty<bool> _isGrounded = new ReactiveProperty<bool>();
        private readonly ReactiveProperty<bool> _isAlive = new ReactiveProperty<bool>();

        public IReadOnlyReactiveProperty<Vector3> Position => _position;
        public IReadOnlyReactiveProperty<bool> IsGrounded => _isGrounded;
        public IReadOnlyReactiveProperty<bool> IsAlive => _isAlive;
        
        public void Jump()
        {
            if (_isGrounded.Value && _isAlive.Value)
            {
                _isGrounded.Value = false;
            }
        }

        public void UpdatePosition(Vector3 position)
        {
            _position.Value = position;
        }

        public void SetGrounded(bool grounded)
        {
            _isGrounded.Value = grounded;
        }

        public void Die()
        {
            _isAlive.Value = false;
        }

        public void ReSet()
        {
            _isAlive.Value = true;
            _isGrounded.Value = true;
            _position.Value = Vector3.zero;
        }
    }
}