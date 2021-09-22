using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class BattleCameraController : BaseController
{
    [Inject(Id = "BattleCamera")]
    private Camera m_mapCamera;


    public override void Initialize()
    {
        m_mapCamera.enabled = false;
    }

    public override void Tick()
    {
        //TODO 
    }
}
