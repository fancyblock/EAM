using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName = "EAM/Game Settings")]
public class SettingInstaller : ScriptableObjectInstaller<SettingInstaller>
{
    public MapConfig m_mapCfg;


    public override void InstallBindings()
    {
        Container.BindInstances(m_mapCfg);
    }
}
