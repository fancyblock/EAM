using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;


public class BoatController : BaseController
{
    [Inject]
    private BoatConfig m_boatCfg;
    [Inject(Id = "BoatInitPos")]
    private Vector2Int m_boatInitPos;


    public override void Initialize()
    {
        //TODO 
    }

    public override void Tick()
    {
        //TODO 
    }

    private void InitBoat()
    {
        //TODO 
    }
}
