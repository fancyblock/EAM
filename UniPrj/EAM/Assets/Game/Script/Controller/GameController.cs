using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameController : BaseController
{
    [Inject]
    private SignalBus m_signalBus;
    [Inject]
    private MapController m_mapController;
    [Inject]
    private BoatController m_boatController;

    private bool m_init = false;


    public override void Initialize()
    {
        Util.Log("Game start.", Color.red);

        Application.targetFrameRate = 60;

        // loadLevel();
    }

    public override void Tick()
    {
        ////////////////////////////////////////[TEMP]
        if (m_init)
            return;

        m_signalBus.Fire(new SignalCreateMap());
        m_signalBus.Fire(new SignalInitBoat());

        m_init = true;
        ////////////////////////////////////////
    }

}
