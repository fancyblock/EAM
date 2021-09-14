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
    [Inject]
    private MapConfig m_mapConfig;
    [Inject(Id = "tile")]
    private GameObject m_tilePrefab;
    [Inject]
    private TileImageConfig m_tileImageConfig;
    [Inject]
    private Tile.Factory m_tileFactory;
    [Inject(Id = "ClickedArea")]
    private ClickedArea m_clickedArea;

    private TableMap m_tableMap;
    private Dictionary<string, TableMapTile> m_tableMapTile;
    private Dictionary<string, TableTileTerrain> m_tableTerrain;
    private List<TableTileShape> m_tableTileShape;

    private Tile[,] m_mapTiles;


    public override void Initialize()
    {
        Util.Log("Map initialize.");

        m_signalBus.Subscribe<SignalCreateMap>(loadMap);
        m_signalBus.Subscribe<SignalTouchMap>(onMapTouch);
    }

    public override void Tick() { }

    public Vector2 Tile2Position(float x, float y)
    {
        Vector2 res = new Vector2();

        float xOffset = (y % 2) * m_mapConfig.m_tileSize.x * 0.5f;          // ż������ƫ
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


    /// <summary>
    /// ��ͼ�鱻����
    /// </summary>
    /// <param name="signal"></param>
    private void onMapTouch(SignalTouchMap signal)
    {
        Vector2 mapPosition = signal.m_position;
        Vector2Int tilePosition = Position2Tile(mapPosition.x, mapPosition.y);
        Vector2 pos = Tile2Position(tilePosition.x, tilePosition.y);

        m_clickedArea.ShowAt(pos.x, pos.y);
    }


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
                Tile tile = m_tileFactory.Create();
                tile.transform.SetParent(m_mapContainer);
                Transform trans = tile.GetComponent<Transform>();

                trans.localPosition = Tile2Position(i, j);

                TableMapTile tmt = m_tableMapTile[m_tableMap.m_tiles[i, j]];
                TableTileTerrain ttt = m_tableTerrain[tmt.terrain];
                tile.SetTile(tmt, ttt);

                // set tile sorting order 
                tile.SetOrder(j);

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
                    Util.Log($"����({i}x{j})�Ҳ�����Ӧ�ı�Եģʽ", Color.red);

                    tile.SetNoMactch();
                }
                else
                {
                    int tileImgIndex = m_tileImageConfig.m_shapeSequence.IndexOf(resultShape.id);
                    TileShape ts = m_tileImageConfig.m_normalTile[tileImgIndex];
                    BaseStone bs = m_tileImageConfig.m_baseStone[tileImgIndex];

                    tile.SetTileImage(ts.m_shape[UnityEngine.Random.Range(0, ts.m_shape.Count)]);   // �������һ��
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

        bool boardDefault = false;           // �߽�Ĭ��û���� [HACK]

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
