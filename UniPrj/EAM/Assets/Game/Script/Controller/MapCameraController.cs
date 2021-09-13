using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class MapCameraController : BaseController
{
    [Inject(Id = "MapCamera")]
    private Camera m_mapCamera;
    [Inject]
    private Boat m_boat;


    public override void Initialize()
    {
        //TODO 
    }

    public override void Tick()
    {
        Vector2 boatPos = m_boat.transform.position;
        Vector2 camPos = m_mapCamera.transform.position;

        if ((boatPos - camPos).magnitude <= float.Epsilon)
            return;

        m_mapCamera.transform.position = camPos + (boatPos - camPos) * 0.7f;
    }
}
