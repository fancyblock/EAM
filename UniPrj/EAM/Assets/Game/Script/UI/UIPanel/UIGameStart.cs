using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class UIGameStart : BaseUIPanel
{
    [Inject(Id = "GameStart")]
    protected override AutoContainer m_ui { get; set; }


    protected override void onOpen()
    {
        m_ui.SetButtonEvent("btnStart", onStart);
    }


    private void onStart()
    {
        m_signalBus.Fire(new SignalCreateMap());

        m_ui.gameObject.SetActive(false);
    }
}
