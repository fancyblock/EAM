using Zenject;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        initSignal();

#if UNITY_EDITOR
        Container.Bind<IGameSettingLoader>().To<EditorGameSettingLoader>().AsSingle();
#else
        Container.Bind<IGameSettingLoader>().To<RuntimeGameSettingLoader>().AsSingle();
#endif

        initFactory();
        initControllers();
    }


    /// <summary>
    /// signal
    /// </summary>
    private void initSignal()
    {
        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<SignalCreateMap>();
        Container.DeclareSignal<SignalInitBoat>();
        Container.DeclareSignal<SignalTouchMap>();
        Container.DeclareSignal<SignalBoatPositionChange>();
    }

    /// <summary>
    /// controller
    /// </summary>
    private void initControllers()
    {
        Container.Bind(typeof(IInitializable), typeof(ITickable), typeof(MapController)).To<MapController>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(ITickable), typeof(GameController)).To<GameController>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(ITickable), typeof(BoatController)).To<BoatController>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(ITickable), typeof(MapCameraController)).To<MapCameraController>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(ITickable), typeof(TouchController)).To<TouchController>().AsSingle();
    }

    private void initFactory()
    {
        Container.BindFactory<Tile, Tile.Factory>().FromComponentInNewPrefabResource("Tile");       //[TEMP]
        Container.BindFactory<Fog, Fog.Factory>().FromComponentInNewPrefabResource("Fog");
    }
}
