using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;
using Zenject;


public class TouchController : BaseController
{
    [Inject(Id = "MapCamera")]
    private Camera m_mapCamera;


    public override void Initialize()
    {
        Lean.Touch.LeanTouch.OnFingerTap += HandleFingerTap;
    }

    public override void Tick()
    {
        //TODO 
    }


    private void HandleFingerTap(LeanFinger finger)
    {
        Vector2 worldPosition = m_mapCamera.ScreenToWorldPoint(finger.ScreenPosition);

        m_signalBus.Fire(new SignalTouchMap() { m_position = worldPosition });
    }
}
