using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameController : ITickable, IInitializable
{
    [Inject]
    private MapController m_mapController;


    public void Initialize()
    {
        Util.Log("Game start.", Color.red);

        Application.targetFrameRate = 60;

        // loadLevel();
    }

    public void Tick()
    {
        //TODO 
    }
}
