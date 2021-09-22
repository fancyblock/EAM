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
        Debug.Log($"{signal.m_uiName}  -   {m_ui.name}");

        if (signal.m_uiName != m_ui.name)
            return;

        switch (signal.m_action)
        {
            case eUIBaseAction.open:
                m_ui.gameObject.SetActive(true);
                onOpen();
                break;
            case eUIBaseAction.close:
                onClose();
                m_ui.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }


    protected virtual void onOpen() { }

    protected virtual void onClose() { }
}
