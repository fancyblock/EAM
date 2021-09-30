using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class UIInstaller : MonoInstaller
{
    [SerializeField]
    private Transform m_uiRoot;
    [SerializeField]
    private List<GameObject> m_uiList;


    public override void InstallBindings()
    {
        Container.BindInstance(m_uiRoot).WithId("UIRoot");

        foreach (GameObject go in m_uiList)
            Container.BindInstance(go.GetComponent<AutoContainer>()).WithId(go.name);

        installUISignal();

        installModel();
        installUIPanel();
        installCommand();
    }


    private void installUISignal()
    {
        Container.DeclareSignal<UICommonSignal>();
    }

    private void installUIPanel()
    {
        Container.Bind(typeof(IInitializable), typeof(UIGameStart)).To<UIGameStart>().AsSingle();
        Container.Bind(typeof(IInitializable), typeof(UIGameHud)).To<UIGameHud>().AsSingle();
    }

    private void installModel()
    {
        Container.Bind<IGameCtxModel>().WithId("model").To<GameCtxModel>().AsSingle();
    }

    private void installCommand()
    {
        bindCommand<GameInitSignal, GameInitCmd>();
        bindCommand<SwitchSceneSignal, SwitchSceneCmd>();
    }


    private void bindCommand<T,U>() where T : BaseSignal where U : BaseCommand
    {
        Container.Bind<U>().AsTransient();
        Container.DeclareSignal<T>().RunAsync();
        Container.BindSignal<T>().ToMethod<U>(x => x.Exe).FromResolve();
    }
}
