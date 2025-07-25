// using UnityEngine;
// using Zenject;
//
// [CreateAssetMenu(fileName = "ArmyInstaller", menuName = "Installers/Army Installer")]
// public class ArmyInstaller : ScriptableObjectInstaller<ArmyInstaller>
// {
//     [SerializeField] private ArmyView armyViewPrefab;
//
//     public override void InstallBindings()
//     {
//         // Bind Model interface to implementation
//         Container.Bind<IArmyModel>()
//             .To<ArmyModel>()
//             .AsTransient();
//
//         // Bind Presenter interface to implementation
//         Container.Bind<IArmyPresenter>()
//             .To<ArmyPresenter>()
//             .AsTransient();
//
//         // Bind Factory
//         Container.Bind<ArmyFactory>()
//             .AsSingle()
//             .WithArguments(armyViewPrefab);
//
//         // Bind Manager
//         Container.Bind<IArmyManager>()
//             .To<ArmyManager>()
//             .AsSingle();
//     }
// }