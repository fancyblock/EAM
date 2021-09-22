using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class UIGameHud : BaseUIPanel
{
    [Inject(Id = "GameHud")]
    protected override AutoContainer m_ui { get; set; }


    protected override void onOpen()
    {
        m_ui.SetButtonEvent("btnStartBattle", onStartBattle);
    }


    private void onStartBattle()
    {
        Util.Log("Start Battle", Color.yellow);

        //TODO 
    }
}
