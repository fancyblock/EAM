using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;


public class Tile : MonoBehaviour
{
    [SerializeField] SortingGroup m_render;
    [SerializeField] GameObject m_tileGo;

    public TableMapTile TILE { get; private set; }

    public TableTileTerrain TERRAIN { get; private set; }


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
        TILE = tableMapTile;
        TERRAIN = tableTerrain;

        switch (tableTerrain.terrain)
        {
            case eTerrain.nil:
                m_tileGo.SetActive(false);          //[TEMP]
                break;
            case eTerrain.ground:
                m_tileGo.SetActive(true);           //[TEMP]
                break;
            default:
                break;
        }
    }

    public bool IS_GROUND
    {
        get
        {
            return TERRAIN.terrain == eTerrain.ground;
        }
    }
}
