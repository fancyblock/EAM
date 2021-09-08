using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Util
{
    static public void Log(string log, Color color = new Color())
    {
        string r = Mathf.RoundToInt((color.r / 1.0f) * 255).ToString("X").PadLeft(2, '0');
        string g = Mathf.RoundToInt((color.g / 1.0f) * 255).ToString("X").PadLeft(2, '0');
        string b = Mathf.RoundToInt((color.b / 1.0f) * 255).ToString("X").PadLeft(2, '0');

        string head = $"<color=#{r}{g}{b}>";
        string tail = "</color>";

        Debug.Log($"{head}log{tail}");
    }
}
