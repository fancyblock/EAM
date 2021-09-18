using UnityEngine;
using Zenject;


public class FogGo : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer m_sprite;

    private eFogType m_type;


    public class Pool : MemoryPool<FogGo>
    {
        protected override void Reinitialize(FogGo item)
        {
        }

        protected override void OnDespawned(FogGo item)
        {
            item.gameObject.SetActive(false);
        }
    }


    public void SetOrder(int order)
    {
        m_sprite.sortingOrder = order;
    }

}
