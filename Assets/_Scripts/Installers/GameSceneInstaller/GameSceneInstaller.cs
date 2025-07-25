using _Scripts.EventBus;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
{
    [SerializeField] private Camera _mainCamera;
    public override void InstallBindings()
    {
        Container.Bind<IEventBus>().To<UniRxEventBus>().AsSingle();
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();

        
    }
}