using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        // game config
        // Container.Bind<MapRoad>().FromMethodMultiple((InjectContext context) => { return context.Container.Resolve<IGameSettingLoader>().LoadData<MapRoad>("MapRoad"); });
        // Container.Bind<MapBuilding>().FromMethodMultiple((InjectContext context) => { return context.Container.Resolve<IGameSettingLoader>().LoadData<MapBuilding>("MapBuilding"); });

        Container.BindFactory<Tile, Tile.Factory>().FromComponentInNewPrefabResource("Tile");       //[TEMP]

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
}
