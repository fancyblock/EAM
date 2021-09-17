using UnityEngine;
using UnityEngine.Rendering;
using Zenject;


public class Tile : MonoBehaviour
{
    [SerializeField] SortingGroup m_render;
    [SerializeField] SpriteRenderer m_tileSprite;
    [SerializeField] SpriteRenderer m_baseStoneSprite;

    public class Factory : PlaceholderFactory<Tile>
    {
    }

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
                m_tileSprite.gameObject.SetActive(false);          //[TEMP]
                break;
            case eTerrain.ground:
            case eTerrain.obstacle:
                m_tileSprite.gameObject.SetActive(true);           //[TEMP]
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

    public void SetNoMactch()
    {
        m_tileSprite.color = Color.red;     //[TEMP]
    }

    public void SetTileImage(Sprite sprite)
    {
        m_tileSprite.sprite = sprite;
    }

    public void SetBaseStoneImage(Sprite sprite)
    {
        if (sprite == null)
        {
            m_baseStoneSprite.gameObject.SetActive(false);
        }
        else
        {
            m_baseStoneSprite.gameObject.SetActive(true);
            m_baseStoneSprite.sprite = sprite;
        }
    }
}
