using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapItem : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;


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
}
