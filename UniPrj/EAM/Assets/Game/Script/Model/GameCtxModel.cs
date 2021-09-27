using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCtxModel : IGameCtxModel
{
    public eScene CURRENT_SCENE { get; set; }


    public void LoadData()
    {
        throw new System.NotImplementedException();
    }

    public void LoadSaver(object saver)
    {
        throw new System.NotImplementedException();
    }

    public List<eUI> GetSceneUI(eScene scene)
    {
        List<eUI> ui = new List<eUI>();

        //TODO 

        return ui;
    }
}
