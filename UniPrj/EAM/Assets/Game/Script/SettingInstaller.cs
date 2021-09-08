using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName = "EAM/Game Settings")]
public class SettingInstaller : ScriptableObjectInstaller<SettingInstaller>
{
    public MapConfig m_mapCfg;
    public TileImageConfig m_tileImageCfg;


    public override void InstallBindings()
    {
        Container.BindInstances(m_mapCfg);
        Container.BindInstances(m_tileImageCfg);
    }
}
