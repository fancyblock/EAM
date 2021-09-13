using System.Collections;
using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;


public class BoatController : BaseController
{
    [Inject]
    private BoatConfig m_boatCfg;
    [Inject(Id = "BoatInitPos")]
    private Vector2Int m_boatInitPos;
    [Inject]
    private Boat m_boat;

    private Vector2 m_destPosition;
    private bool m_moving = false;


    public override void Initialize()
    {
        m_signalBus.Subscribe<SignalInitBoat>(initBoat);
        m_signalBus.Subscribe<SignalTouchMap>(onMapTouch);
    }


    public override void Tick()
    {
        if(m_moving)
        {
            //////////////////////////////////////////[TEMP]
            Vector2 boatPos = m_boat.transform.position;
            Vector2 destPos = m_destPosition;

            if((boatPos - destPos).magnitude <= float.Epsilon)
            {
                m_moving = false;
            }
            else
            {
                m_boat.transform.position = boatPos + (destPos - boatPos).normalized * m_boatCfg.m_normalVelocity.x * Time.deltaTime;
            }
        }
    }

    private void initBoat()
    {
        Util.Log("Crate Boat", Color.green);

        m_boat.SetPosition(m_boatInitPos.x, m_boatInitPos.y);
    }

    private void onMapTouch(SignalTouchMap signal)
    {
        Util.Log($"Goto there {signal.m_position.ToString()}", Color.blue);

        m_destPosition = signal.m_position;
        m_moving = true;
    }
}
