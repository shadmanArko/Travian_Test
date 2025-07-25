// using Cysharp.Threading.Tasks;
// using UniRx;
// using UnityEngine;
//
// public class ArmyModel : IArmyModel
// {
//     public IReadOnlyReactiveProperty<float> Health => health;
//     public IReadOnlyReactiveProperty<Vector3> Position => position;
//     public IReadOnlyReactiveProperty<bool> IsMoving => isMoving;
//
//     private ReactiveProperty<float> health = new ReactiveProperty<float>(100f);
//     private ReactiveProperty<Vector3> position = new ReactiveProperty<Vector3>();
//     private ReactiveProperty<bool> isMoving = new ReactiveProperty<bool>();
//
//     private float maxHealth = 100f;
//     private float moveSpeed = 5f;
//
//     public async UniTask InitializeAsync()
//     {
//         health.Value = maxHealth;
//         isMoving.Value = false;
//         await UniTask.Yield();
//     }
//
//     public void TakeDamage(float damage)
//     {
//         health.Value = Mathf.Max(0, health.Value - damage);
//     }
//
//     public void MoveTo(Vector3 targetPosition)
//     {
//         position.Value = targetPosition;
//         isMoving.Value = true;
//     }
//
//     public void Reset()
//     {
//         health.Value = maxHealth;
//         isMoving.Value = false;
//         position.Value = Vector3.zero;
//     }
//
//     public void Dispose()
//     {
//         health?.Dispose();
//         position?.Dispose();
//         isMoving?.Dispose();
//     }
// }