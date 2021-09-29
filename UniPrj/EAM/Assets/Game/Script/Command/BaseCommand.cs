using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public abstract class BaseCommand 
{
    [Inject]
    protected SignalBus m_signalBus;

    // public abstract void Exe(object param = null);
}
