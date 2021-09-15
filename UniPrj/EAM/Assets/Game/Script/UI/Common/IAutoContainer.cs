using System;
using UnityEngine;

public interface IAutoContainer  
{
    void SetEnable(bool enable);
    int LAST_BTN_FRAME { get; set; }

    IAutoWidget GetWidget(string name);
    T GetWidget<T>(string name);

    void SetEventEnable(bool enable);

    bool IsVirtual(string widgetName);
    void SetEnable(string widgetName, bool enable);
    void SetValue(string widgetName, string text);
    void SetValue(string widgetName, bool toggle);
    void SetValue(string widgetName, float progress);
    void SetButtonEvent(string widgetName, Action evt);
    void SetButtonEnable(string widgetName, bool enable);
    void SetImageFillAmount(string widgetName, float value);
    float GetImageFillAmount(string widgetName);
    void PlayAnimation(string widgetName, string aniName);

    void SetButtonClkHook(Action<string> hook);

    GameObject GetExObject(int index);
}
