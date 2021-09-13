using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


[CreateAssetMenu(menuName = "EAM/Boat Settings")]
public class BoatConfigInstaller : ScriptableObjectInstaller<BoatConfigInstaller>
{
    public BoatConfig m_boatCfg;
    public Vector2Int m_startTile;


    public override void InstallBindings()
    {
        Container.BindInstance(m_boatCfg);
        Container.BindInstance(m_startTile).WithId("BoatInitPos");
    }
}
