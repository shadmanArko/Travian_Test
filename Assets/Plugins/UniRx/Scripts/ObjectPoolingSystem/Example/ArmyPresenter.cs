// using Cysharp.Threading.Tasks;
// using UniRx;
//
// public class ArmyPresenter : IArmyPresenter
// {
//     private readonly IArmyModel model;
//     private IArmyView view;
//     private CompositeDisposable disposables = new CompositeDisposable();
//
//     public ArmyPresenter(IArmyModel model)
//     {
//         this.model = model;
//     }
//
//     public void SetView(IArmyView view)
//     {
//         this.view = view;
//     }
//
//     public async UniTask InitializeAsync()
//     {
//         if (view == null) return;
//
//         // Bind model changes to view updates
//         model.Health
//             .Subscribe(health => view.UpdateHealthVisual(health / 100f))
//             .AddTo(disposables);
//
//         model.IsMoving
//             .Subscribe(isMoving => view.PlayMoveAnimation(isMoving))
//             .AddTo(disposables);
//
//         model.Position
//             .Subscribe(position => view.SetPosition(position))
//             .AddTo(disposables);
//
//         await UniTask.Yield();
//     }
//
//     public void Reset()
//     {
//         disposables.Clear();
//         disposables = new CompositeDisposable();
//     }
//
//     public void Dispose()
//     {
//         disposables?.Dispose();
//     }
// }