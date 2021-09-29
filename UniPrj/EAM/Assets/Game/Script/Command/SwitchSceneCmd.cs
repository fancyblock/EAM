using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class SwitchSceneCmd : BaseCommand
{
    [Inject]
    public void Exe(SwitchSceneSignal signal)
    {
        //TODO 

        Util.Log(signal.SCENE.ToString() + "   <><><>");
    }
}
