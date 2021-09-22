using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class UIGameHud : BaseUIPanel
{
    [Inject(Id = "GameStart")]
    protected override AutoContainer m_ui { get; set; }


}
