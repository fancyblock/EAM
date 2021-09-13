using Zenject;


public class BaseController : ITickable, IInitializable
{
    [Inject]
    protected SignalBus m_signalBus;


    public virtual void Initialize() { }

    public virtual void Tick() { }
}
