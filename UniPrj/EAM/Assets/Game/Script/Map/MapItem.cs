using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;

    [Inject]
    private MapImageConfig m_mapImageCfg;

    private TableMapItem m_mapItem;


    public class Factory : PlaceholderFactory<MapItem>
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void SetOrder(int order)
    {
        m_sprite.sortingOrder = order;
    }

    public void SetMapItem(TableMapItem mapItem)
    {
        m_mapItem = mapItem;

        m_sprite.sprite = null;

        foreach (var spr in m_mapImageCfg.m_mapItemImages)
        {
            if (spr.name == m_mapItem.display)
            {
                m_sprite.sprite = spr;
                break;
            }
        }
    }

}
