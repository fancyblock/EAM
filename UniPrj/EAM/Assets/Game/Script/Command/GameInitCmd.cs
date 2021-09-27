using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInitCmd : BaseCommand
{
    public override void Exe()
    {
        Util.Log("Game Start", Color.red);

        Application.targetFrameRate = 60;

        //TODO 

        m_signalBus.Fire(new UICommonSignal() { m_action = eUIBaseAction.open, m_uiName = eUI.GameStart });
        m_signalBus.Fire(new UICommonSignal() { m_action = eUIBaseAction.open, m_uiName = eUI.GameHud });
    }
}
