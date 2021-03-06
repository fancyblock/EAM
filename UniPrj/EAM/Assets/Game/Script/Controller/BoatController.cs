using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;


public class BoatController : BaseController
{
    [Inject]
    private MapController m_mapController;
    [Inject]
    private BoatConfig m_boatCfg;
    [Inject(Id = "BoatInitPos")]
    private Vector2Int m_boatInitPos;
    [Inject]
    private Boat m_boat;


    public override void Initialize()
    {
        m_signalBus.Subscribe<SignalInitBoat>(initBoat);
        m_signalBus.Subscribe<SignalTouchMap>(onMapTouch);
    }


    public override void Tick()
    {
        //TODO 
    }

    private void initBoat()
    {
        m_boat.SetPosition(m_boatInitPos.x, m_boatInitPos.y);
        m_boat.SetVelocity(m_boatCfg.m_normalVelocity);
    }

    private void onMapTouch(SignalTouchMap signal)
    {
        Vector2Int tilePos = m_mapController.Position2Tile(signal.m_position.x, signal.m_position.y);

        Util.Log($"Goto there {tilePos.ToString()}", new Color(0.1f, 0.33f, 0.85f));

        Tile tile = m_mapController.GetTile(tilePos.x, tilePos.y);

        if (tile.TERRAIN == eTerrain.obstacle)      //[TEMP]
            return;

        m_boat.MoveTo(tilePos.x, tilePos.y);
    }
}
