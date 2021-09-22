using System.Collections.Generic;
using Zenject;
using UnityEngine;
using System;

public class MapController : BaseController
{
    [Inject]
    private IGameSettingLoader m_loader;

    [Inject(Id = "MapContainer")]
    private Transform m_mapContainer;
    [Inject(Id = "CloudContainer")]
    private Transform m_fogContainer;

    [Inject]
    private MapConfig m_mapConfig;
    [Inject]
    private TileImageConfig m_tileImageConfig;

    [Inject]
    private Tile.Factory m_tileFactory;
    [Inject]
    private Fog.Factory m_fogFactory;
    [Inject]
    private City.Factory m_cityFactory;
    [Inject]
    private MapItem.Factory m_mapItemFactory;

    [Inject(Id = "ClickedArea")]
    private ClickedArea m_clickedArea;

    private TableMap m_tableMap;
    private Dictionary<string, TableMapTile> m_tableMapTile;
    private Dictionary<string, TableTileTerrain> m_tableTerrain;
    private Dictionary<string, TableCity> m_tableCity;
    private Dictionary<string, TableMapItem> m_tableMapItem;
    private List<TableTileShape> m_tableTileShape;

    private Tile[,] m_mapTiles;
    private Fog[,] m_mapFogs;


    public override void Initialize()
    {
        Util.Log("Map initialize.");

        m_signalBus.Subscribe<SignalCreateMap>(loadMap);
        m_signalBus.Subscribe<SignalTouchMap>(onMapTouch);
        m_signalBus.Subscribe<SignalBoatPositionChange>(onBoatPositionChange);
    }

    public override void Tick() { }

    public Vector2 Tile2Position(float x, float y)
    {
        Vector2 res = new Vector2();

        float xOffset = (y % 2) * m_mapConfig.m_tileSize.x * 0.5f;          // 偶数行右偏
        res.x = x * m_mapConfig.m_tileSize.x + xOffset;
        res.y = -y * m_mapConfig.m_tileSize.y;

        return res;
    }

    public Vector2Int Position2Tile(float x, float y)
    {
        Vector2Int res = new Vector2Int();

        res.y = Mathf.RoundToInt(-y / m_mapConfig.m_tileSize.y);

        if (res.y % 2 == 0)
            res.x = Mathf.RoundToInt(x / m_mapConfig.m_tileSize.x);
        else
            res.x = Mathf.RoundToInt(x / m_mapConfig.m_tileSize.x - 0.5f);

        return res;
    }

    public Tile GetTile(int x, int y)
    {
        Tile tile = null;

        try
        {
            tile = m_mapTiles[x, y];
        }
        catch(Exception e)
        {
        }

        return tile;
    }


    /// <summary>
    /// 地图块被点中
    /// </summary>
    /// <param name="signal"></param>
    private void onMapTouch(SignalTouchMap signal)
    {
        Vector2 mapPosition = signal.m_position;
        Vector2Int tilePosition = Position2Tile(mapPosition.x, mapPosition.y);
        Vector2 pos = Tile2Position(tilePosition.x, tilePosition.y);

        m_clickedArea.ShowAt(pos.x, pos.y);
    }

    /// <summary>
    /// 飞船位置改变
    /// </summary>
    /// <param name="param"></param>
    private void onBoatPositionChange(SignalBoatPositionChange param)
    {
        updateTileDisplay(param.X, param.Y);
        refreshFog(param.X, param.Y, param.MapPosition);
    }

    private void updateTileDisplay(int x, int y)
    {
        int xRange = 4;     /////////////////////[TEMP]
        int yRange = 27;    

        for(int i = x - xRange; i <= x + xRange; i++)
        {
            for(int j = y - yRange; j <= y + yRange; j++)
            {
                if (i >= 0 && j >= 0 && i < m_tableMap.m_width && j < m_tableMap.m_height)
                    m_mapTiles[i, j].ACTIVE_DISPLAY = true;
            }
        }
    }

    /// <summary>
    /// 刷新迷雾
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private void refreshFog(int x, int y, Vector2 mapPosition)
    {
        int xRange = 4;
        int yRange = 27;     /////////////////////[TEMP]

        for(int i = x - xRange; i <= x + xRange; i++)
        {
            for(int j = y - yRange; j <= y + yRange; j++)
            {
                if (i >= 0 && j >= 0 && i < m_tableMap.m_width && j < m_tableMap.m_height)
                {
                    Fog fog = m_mapFogs[i, j];
                    fog.ACTIVE_DISPLAY = true;
                    fog.RefreshFog(i, j, mapPosition, m_mapConfig.m_fogRadius);
                }
            }
        }
    }

    /// <summary>
    /// 加载地图
    /// </summary>
    private void loadMap()
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

        var rawTableCity = m_loader.LoadData<TableCity>("TableCity");
        m_tableCity = new Dictionary<string, TableCity>();
        foreach (var c in rawTableCity)
            m_tableCity.Add(c.id, c);

        var rawTableMapItem = m_loader.LoadData<TableMapItem>("TableMapItem");
        m_tableMapItem = new Dictionary<string, TableMapItem>();
        foreach (var mi in rawTableMapItem)
            m_tableMapItem.Add(mi.id, mi);

        createMap();
        refreshTileCorner();

        m_signalBus.Fire(new SignalInitBoat());         //////////////////////////////////////[TEMP]
    }

    private void createMap()
    {
        m_mapTiles = new Tile[m_tableMap.m_width, m_tableMap.m_height];
        m_mapFogs = new Fog[m_tableMap.m_width, m_tableMap.m_height];

        for (int i = 0; i < m_tableMap.m_width; i++)
        {
            for(int j = 0; j < m_tableMap.m_height; j++)
            {
                TableMapTile tmt = m_tableMapTile[m_tableMap.m_tiles[i, j]];

                // tile
                Tile tile = m_tileFactory.Create();
                tile.SetTransformParent(m_mapContainer);
                tile.SetPosition(Tile2Position(i, j));
                tile.SetTile(tmt, m_tableTerrain[tmt.terrain]);
                tile.SetOrder(j);

                m_mapTiles[i, j] = tile;

                // city
                if(!string.IsNullOrEmpty(tmt.cityId) && tmt.cityId != "nil")
                {
                    City city = m_cityFactory.Create();
                    city.transform.SetParent(m_mapContainer);
                    city.transform.localPosition = Tile2Position(i, j);

                    city.SetCity(m_tableCity[tmt.cityId]);
                    city.SetOrder(j + m_mapConfig.m_mapItemOrderOffset);
                }
                // item
                else if(!string.IsNullOrEmpty(tmt.itemId) && tmt.itemId != "nil")
                {
                    MapItem mapItem = m_mapItemFactory.Create();
                    mapItem.transform.SetParent(m_mapContainer);
                    mapItem.transform.localPosition = Tile2Position(i, j);

                    mapItem.SetMapItem(m_tableMapItem[tmt.itemId]);
                    mapItem.SetOrder(j + m_mapConfig.m_mapItemOrderOffset);
                }

                // fog
                Fog fog = m_fogFactory.Create();
                fog.SetTransformParent(m_fogContainer);
                fog.SetPosition(Tile2Position(i, j));

                fog.SetFog(tmt.fog);
                fog.SetOrder(j + m_mapConfig.m_fogOrderOffset);

                m_mapFogs[i, j] = fog;
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

                TableTileShape tts = getTileAroundInfo(i, j);
                TableTileShape resultShape = null;

                foreach(TableTileShape shape in m_tableTileShape)
                {
                    if(tts.down == shape.down && tts.left == shape.left && tts.right == shape.right && tts.up == shape.up &&
                        tts.left_down == shape.left_down && tts.left_up == shape.left_up &&
                        tts.right_down == shape.right_down && tts.right_up == shape.right_up )
                    {
                        resultShape = shape;
                        break;
                    }
                }

                if(resultShape == null)
                {
                    Util.Log($"坐标({i}x{j})找不到对应的边缘模式", Color.red);
#if UNITY_EDITOR
                    tile._SetNoMatch();
#endif
                }
                else
                {
                    int tileImgIndex = m_tileImageConfig.m_shapeSequence.IndexOf(resultShape.id);
                    TileShape ts = m_tileImageConfig.m_normalTile[tileImgIndex];
                    BaseStone bs = m_tileImageConfig.m_baseStone[tileImgIndex];

                    tile.SetTileImage(ts.m_shape[UnityEngine.Random.Range(0, ts.m_shape.Count)]);   // 随机给出一个
                    tile.SetBaseStoneImage(bs.m_baseStone.Count == 0 ? null : bs.m_baseStone[UnityEngine.Random.Range(0, bs.m_baseStone.Count)]);
                }
            }
        }
    }

    private TableTileShape getTileAroundInfo(int x, int y)
    {
        TableTileShape tts = new TableTileShape();

        Tile right = getTile(x + 1, y);
        Tile left = getTile(x - 1, y);
        Tile up = getTile(x, y - 2);
        Tile down = getTile(x, y + 2);

        Tile rightUp = null, rightDown = null;
        Tile leftUp = null, leftDown = null;

        if( y % 2 == 0 )
        {
            rightUp = getTile(x, y - 1);
            rightDown = getTile(x, y + 1);
            leftUp = getTile(x - 1, y - 1);
            leftDown = getTile(x - 1, y + 1);
        }
        else
        {
            rightUp = getTile(x + 1, y - 1);
            rightDown = getTile(x + 1, y + 1);
            leftUp = getTile(x, y - 1);
            leftDown = getTile(x, y + 1);
        }

        bool boardDefault = false;           // 边界默认没东西 [HACK]

        tts.right = right == null ? boardDefault : right.IS_GROUND;
        tts.left = left == null ? boardDefault : left.IS_GROUND;
        tts.up = up == null ? boardDefault : up.IS_GROUND;
        tts.down = down == null ? boardDefault : down.IS_GROUND;
        tts.right_up = rightUp == null ? boardDefault : rightUp.IS_GROUND;
        tts.right_down = rightDown == null ? boardDefault : rightDown.IS_GROUND;
        tts.left_up = leftUp == null ? boardDefault : leftUp.IS_GROUND;
        tts.left_down = leftDown == null ? boardDefault : leftDown.IS_GROUND;

        return tts;
    }

    private Tile getTile(int x, int y)
    {
        if(x >= 0 && x < m_tableMap.m_width &&
            y >= 0 && y < m_tableMap.m_height )
        {
            return m_mapTiles[x, y];
        }

        return null;
    }
}
