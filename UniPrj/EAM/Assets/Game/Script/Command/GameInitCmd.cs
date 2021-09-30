using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class GameInitCmd : BaseCommand
{
    [Inject]
    private DiContainer m_container;


    public override void Exe(BaseSignal signal)
    {
        Util.Log("Game Start", Color.red);

        Application.targetFrameRate = 60;

        // initial all model
        //List<IBaseModel> models = (List<IBaseModel>)m_container.ResolveIdAll(typeof(IBaseModel), "model");
        //Debug.Log($"model count: {models.Count}");

        //foreach(IBaseModel model in models)
        //{
        //    model.LoadData();
        //}

        m_signalBus.Fire(new SwitchSceneSignal() { SCENE = eScene.Loading });
    }
}
