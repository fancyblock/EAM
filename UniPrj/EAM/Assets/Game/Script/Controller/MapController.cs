using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;

public class MapController : IInitializable
{
    [Inject]
    private IGameSettingLoader m_loader;
    [Inject(Id = "MapCamera")]
    private Camera m_mapCamera;
    [Inject(Id = "MapContainer")]
    private Transform m_mapContainer;
    [Inject]
    private MapConfig m_mapConfig;
    [Inject(Id = "tile")]
    private GameObject m_tilePrefab;

    private TableMap m_tableMap;
    private List<TableMapTile> m_tableMapTile;


    public void Initialize()
    {
        Util.Log("Map initialize.");

        //TODO 

        LoadMap();
    }

    public void LoadMap()
    {
        Util.Log("LoadMap");

        m_tableMapTile = m_loader.LoadData<TableMapTile>("TableMapTile");
        m_tableMap = new TableMap();

        object[,] rawTableMap = m_loader.LoadRawSheet("TableMap");

        m_tableMap.m_width = rawTableMap.GetLength(0);
        m_tableMap.m_height = rawTableMap.GetLength(1);
        m_tableMap.m_tiles = new string[m_tableMap.m_width, m_tableMap.m_height];

        for(int i = 0; i < m_tableMap.m_width; i++)
        {
            for(int j = 0; j < m_tableMap.m_height; j++)
                m_tableMap.m_tiles[i,j] = rawTableMap[i, j].ToString();
        }

        createMap();
    }


    private void createMap()
    {
        //TODO 

        for(int i = 0; i < m_tableMap.m_width; i++)
        {
            for(int j = 0; j < m_tableMap.m_height; j++)
            {
                GameObject go = GameObject.Instantiate(m_tilePrefab, m_mapContainer);
                Transform trans = go.GetComponent<Transform>();

                float xOffset = (j % 2) * m_mapConfig.m_tileSize.x * 0.5f;
                trans.localPosition = new Vector2(i * m_mapConfig.m_tileSize.x + xOffset, -j * m_mapConfig.m_tileSize.y);

                //TODO 
            }
        }
    }
}
