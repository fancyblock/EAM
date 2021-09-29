using UnityEngine;
using Zenject;


public class BaseUIPanel : IInitializable
{
    [Inject]
    protected SignalBus m_signalBus;

    protected virtual AutoContainer m_ui { get; set; }


    public void Initialize()
    {
        m_signalBus.Subscribe<UICommonSignal>(onUICommonSignal);
    }


    private void onUICommonSignal(UICommonSignal signal)
    {
        if (signal.m_uiName.ToString() != m_ui.name)
            return;

        switch (signal.m_action)
        {
            case eUIBaseAction.open:
                onOpen();
                break;
            case eUIBaseAction.close:
                onClose();
                break;
            default:
                break;
        }
    }


    protected virtual void onEnterScene(eScene scene) { }

    protected virtual void onExitScene(eScene scene) { }

    protected virtual void onOpen() { }

    protected virtual void onClose() { }
}
