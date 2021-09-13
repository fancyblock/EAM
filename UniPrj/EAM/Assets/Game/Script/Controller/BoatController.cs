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
        m_signalBus.Subscribe<SignalInitBoat>(initBoat);
    }

    public override void Tick()
    {
        //TODO 
    }

    private void initBoat()
    {
        Util.Log("Crate Boat", Color.green);

        //TODO 
    }
}
