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
    [Inject]
    private TileImageConfig m_tileImageConfig;

    private TableMap m_tableMap;
    private Dictionary<string, TableMapTile> m_tableMapTile;
    private Dictionary<string, TableTileTerrain> m_tableTerrain;
    private List<TableTileShape> m_tableTileShape;

    private Tile[,] m_mapTiles;


    public void Initialize()
    {
        Util.Log("Map initialize.");

        //TODO 

        LoadMap();
    }

    public void LoadMap()
    {
        Util.Log("LoadMap");

        var rawMapTile = m_loader.LoadData<TableMapTile>("TableMapTile");
        m_tableMapTile = new Dictionary<string, TableMapTile>();
        foreach (var mt in rawMapTile)
            m_tableMapTile.Add(mt.id, mt);

        var rawTerrain = m_loader.LoadData<TableTileTerrain>("TableTileTerrain");
        m_tableTerrain = new Dictionary<string, TableTileTerrain>();
        foreach (var tt in rawTerrain)
            m_tableTerrain.Add(tt.id, tt);

        m_tableTileShape = m_loader.LoadData<TableTileShape>("TableTileShape");

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
        refreshTileCorner();
    }


    private void createMap()
    {
        m_mapTiles = new Tile[m_tableMap.m_width, m_tableMap.m_height];

        for (int i = 0; i < m_tableMap.m_width; i++)
        {
            for(int j = 0; j < m_tableMap.m_height; j++)
            {
                GameObject go = GameObject.Instantiate(m_tilePrefab, m_mapContainer);
                Transform trans = go.GetComponent<Transform>();

                float xOffset = ((j + 1) % 2) * m_mapConfig.m_tileSize.x * 0.5f;          // Å¼ÊýÐÐÓÒÆ«
                trans.localPosition = new Vector2(i * m_mapConfig.m_tileSize.x + xOffset, -j * m_mapConfig.m_tileSize.y);

                Tile tile = go.GetComponent<Tile>();
                TableMapTile tmt = m_tableMapTile[m_tableMap.m_tiles[i, j]];
                TableTileTerrain ttt = m_tableTerrain[tmt.terrain];
                tile.SetTile(tmt, ttt);

                // set tile sorting order 
                //TODO 

                m_mapTiles[i, j] = tile;
            }
        }
    }

    private void refreshTileCorner()
    {
        for (int i = 0; i < m_tableMap.m_width; i++)
        {
            for (int j = 0; j < m_tableMap.m_height; j++)
            {
                Tile tile = m_mapTiles[i, j];

                if (!tile.IS_GROUND)
                    continue;

                //TODO 
            }
        }
    }

    private TableTileShape getTileAroundInfo(int x, int y)
    {
        TableTileShape tts = new TableTileShape();

        //TODO 

        return tts;
    }
}
