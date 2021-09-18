using UnityEngine;
using Zenject;


public class City : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;

    [Inject]
    private MapImageConfig m_mapImageCfg;

    private TableCity m_city;


    public class Factory : PlaceholderFactory<City>
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

    public void SetCity(TableCity city)
    {
        m_city = city;

        m_sprite.sprite = null;

        foreach(var spr in m_mapImageCfg.m_cityImages)
        {
            if(spr.name == city.display)
            {
                m_sprite.sprite = spr;
                break;
            }
        }
    }
}
