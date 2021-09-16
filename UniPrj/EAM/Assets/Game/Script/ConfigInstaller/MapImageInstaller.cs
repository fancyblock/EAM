using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName = "EAM/MapImage Settings")]
public class MapImageInstaller : ScriptableObjectInstaller<BoatConfigInstaller>
{
    public MapImageConfig m_mapImageCfg;


    public override void InstallBindings()
    {
        Container.BindInstance(m_mapImageCfg);
    }
}
