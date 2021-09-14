using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Zenject;


public class Fog : MonoBehaviour
{
    [SerializeField]
    private SortingGroup m_fog;

    private eFogType m_type;
    private Vector2Int m_position;


    public class Factory : PlaceholderFactory<Fog>
    {
    }


    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetFog(eFogType type, int x, int y)
    {
        m_type = type;
        m_position = new Vector2Int(x, y);

        gameObject.SetActive(type != eFogType.nil); 
    }

    public void SetOrder(int order)
    {
        m_fog.sortingOrder = order;
    }

    public void RefreshDisplay(int x, int y, int radius)
    {
        if (m_type == eFogType.nil)
            return;

        gameObject.SetActive((new Vector2(x,y) - new Vector2(m_position.x, m_position.y)).magnitude > radius);
    }
}
