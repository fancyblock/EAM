using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

#if UNITY_EDITOR
        Container.Bind<IGameSettingLoader>().To<EditorGameSettingLoader>().AsSingle();
#else
        Container.Bind<IGameSettingLoader>().To<RuntimeGameSettingLoader>().AsSingle();
#endif

        // game config
        //    Container.Bind<MapRoad>().FromMethodMultiple((InjectContext context) => { return context.Container.Resolve<IGameSettingLoader>().LoadData<MapRoad>("MapRoad"); });
        //    Container.Bind<MapBuilding>().FromMethodMultiple((InjectContext context) => { return context.Container.Resolve<IGameSettingLoader>().LoadData<MapBuilding>("MapBuilding"); });

        Container.Bind(typeof(ITickable), typeof(IInitializable), typeof(GameController)).To<GameController>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(MapController)).To<MapController>().AsSingle();

    }
}
