using UnityEngine;
using UnityEngine.Rendering;
using Zenject;


public class TileGo : MonoBehaviour
{
    [SerializeField] SortingGroup m_render;
    [SerializeField] SpriteRenderer m_tileSprite;
    [SerializeField] SpriteRenderer m_baseStoneSprite;

    public class Pool : MemoryPool<TileGo>
    {
        protected override void Reinitialize(TileGo item)
        {
            item.m_baseStoneSprite.sprite = null;
        }

        protected override void OnDespawned(TileGo item)
        {
            item.gameObject.SetActive(false);
        }
    }



    public void SetOrder(int order)
    {
        m_render.sortingOrder = order;
    }

    public void SetTileSprite(Sprite sprite)
    {
        m_tileSprite.sprite = sprite;
    }

    public void SetBaseStoneSprite(Sprite sprite)
    {
        m_baseStoneSprite.sprite = sprite;
    }

#if UNITY_EDITOR

    public void SetTileColor(Color color)
    {
        m_tileSprite.color = color;
    }

#endif

}
