using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName = "EAM/Map Settings")]
public class SettingInstaller : ScriptableObjectInstaller<SettingInstaller>
{
    public MapConfig m_mapCfg;
    public TileImageConfig m_tileImageCfg;


    public override void InstallBindings()
    {
        Container.BindInstance(m_mapCfg);
        Container.BindInstance(m_tileImageCfg);
    }
}
