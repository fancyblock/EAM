using UnityEngine;
using Zenject;


public class Tile 
{
    [Inject]
    private TileGo.Pool m_pool;

    public class Factory : PlaceholderFactory<Tile> { }


    private Transform m_goParent;
    private Vector2 m_position;
    private int m_sortingOrder;
    private Sprite m_tileSprite;
    private Sprite m_baseStoneSpirite;

    private TableMapTile m_tableMapTile;
    private TableTileTerrain m_tableTileTerrain;

    private bool m_active = false;
    private TileGo m_tileGo = null;


    public bool IS_GROUND
    {
        get { return m_tableTileTerrain.terrain == eTerrain.ground; }
    }

    public eTerrain TERRAIN
    {
        get { return m_tableTileTerrain.terrain; }
    }

    public bool ACTIVE_DISPLAY
    {
        set
        {
            if (TERRAIN == eTerrain.nil)    // 空格不创建任何显示单元
                return;

            if (value == m_active)
                return;

            m_active = value;

            if(m_active)
            {
                m_tileGo = m_pool.Spawn();

#if UNITY_EDITOR
                if (_noMatch)
                    m_tileGo.SetTileColor(Color.red);
#endif

                m_tileGo.transform.SetParent(m_goParent);
                m_tileGo.transform.localPosition = m_position;
                m_tileGo.SetOrder(m_sortingOrder);
                m_tileGo.SetTileSprite(m_tileSprite);
                m_tileGo.SetBaseStoneSprite(m_baseStoneSpirite);
            }
            else
            {
                m_pool.Despawn(m_tileGo);
                m_tileGo = null;
            }
        }
    }

    public void SetTransformParent(Transform trans)
    {
        m_goParent = trans;
    }

    public void SetPosition(Vector2 position)
    {
        m_position = position;
    }

    public void SetTile(TableMapTile tableMapTile, TableTileTerrain tableTileTerrain)
    {
        m_tableMapTile = tableMapTile;
        m_tableTileTerrain = tableTileTerrain;
    }

    public void SetOrder(int order)
    {
        m_sortingOrder = order;
    }

    public void SetTileImage(Sprite sprite)
    {
        m_tileSprite = sprite;
    }

    public void SetBaseStoneImage(Sprite sprite)
    {
        m_baseStoneSpirite = sprite;
    }


#if UNITY_EDITOR

    private bool _noMatch = false;

    public void _SetNoMatch()
    {
        _noMatch = true;
    }

#endif

}
