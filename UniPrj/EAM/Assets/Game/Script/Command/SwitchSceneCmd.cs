using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class SwitchSceneCmd : BaseCommand
{
    public override void Exe(BaseSignal signal)
    {
        //TODO 

        Util.Log((signal as SwitchSceneSignal).SCENE.ToString() + "   <><><>");
    }
}
