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
        installUIPanel();
    }


    private void installUISignal()
    {
        Container.DeclareSignal<UICommonSignal>();
    }

    private void installUIPanel()
    {
        Container.Bind(typeof(IInitializable), typeof(UIGameStart)).To<UIGameStart>().AsSingle();
    }
}
