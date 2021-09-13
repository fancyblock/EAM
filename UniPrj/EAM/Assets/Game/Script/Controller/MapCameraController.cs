using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class MapCameraController : BaseController
{
    [Inject(Id = "MapCamera")]
    private Camera m_mapCamera;


    public override void Initialize()
    {
        //TODO 
    }

    public override void Tick()
    {
        //TODO 
    }
}
