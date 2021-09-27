using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IGameCtxModel : IBaseModel
{
    eScene CURRENT_SCENE { get; set; }

    List<eUI> GetSceneUI(eScene scene);
}
