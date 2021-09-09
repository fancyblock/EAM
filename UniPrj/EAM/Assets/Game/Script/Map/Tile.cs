using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Tile : MonoBehaviour
{
    [SerializeField] SortingGroup m_render;
    [SerializeField] GameObject m_tileGo;


    // Start is called before the first frame update
    void Start()
    {
    }

    public void SetOrder(int order)
    {
        m_render.sortingOrder = order;
    }

    public void SetTile(TableMapTile tableMapTile, TableTileTerrain tableTerrain)
    {
        //TODO 

        switch (tableTerrain.terrain)
        {
            case eTerrain.nil:
                m_tileGo.SetActive(false);
                break;
            case eTerrain.ground:
                m_tileGo.SetActive(true);
                break;
            case eTerrain.block:
                //TODO 
                break;
            default:
                break;
        }
    }
}
