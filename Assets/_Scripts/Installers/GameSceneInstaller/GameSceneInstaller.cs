using _Scripts.Configs;
using _Scripts.EventBus;
using _Scripts.Factory.Obstacle;
using _Scripts.ObstacleSystem.Model;
using _Scripts.ObstacleSystem.Presenter;
using _Scripts.ObstacleSystem.View;
using _Scripts.PlayerSystem.Model;
using _Scripts.PlayerSystem.Presenter;
using _Scripts.PlayerSystem.View;
using _Scripts.ScoreSystem.Model;
using _Scripts.ScoreSystem.Presenter;
using _Scripts.ScoreSystem.View;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "GameSceneInstaller", menuName = "Installers/GameSceneInstaller")]
public class GameSceneInstaller : ScriptableObjectInstaller<GameSceneInstaller>
{
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private ObstacleView _obstacleView;
    [SerializeField] private GameConfig gameConfig;
    
    [SerializeField] private PlayerView playerView;
    [SerializeField] private ScoreView scoreView;
    
    public override void InstallBindings()
    {
        // Core services
        Container.Bind<IEventBus>().To<UniRxEventBus>().AsSingle();
        Container.Bind<Camera>().FromInstance(_mainCamera).AsSingle();
        Container.Bind<GameConfig>().FromScriptableObject(gameConfig).AsSingle();
        
        // Obstacle System
        Container.Bind<ObstacleView>().FromComponentInNewPrefab(_obstacleView).AsTransient();
        Container.Bind<IObstaclePresenter>().To<ObstaclePresenter>().AsTransient();
        Container.Bind<IObstacleModel>().To<ObstacleModel>().AsTransient();
        
        // Player System
        Container.Bind<IPlayerModel>().To<PlayerModel>().AsSingle();
        Container.Bind<PlayerView>().FromComponentInNewPrefab(playerView).AsSingle();
        Container.Bind<IPlayerPresenter>().To<PlayerPresenter>().AsSingle();
        
        // Score System
        Container.Bind<IScoreModel>().To<ScoreModel>().AsSingle();
        Container.Bind<ScoreView>().FromComponentInNewPrefab(scoreView).AsSingle();
        Container.Bind<IScorePresenter>().To<ScorePresenter>().AsSingle();
        
        // Factory
        Container.BindFactory<ObstacleView, GameConfig, IObstaclePresenter, ObstaclePresenterFactory>()
                 .FromFactory<ObstaclePresenterFactory>();
    }
}