using UnityEngine;
using Zenject;


public class Fog 
{
    [Inject]
    private FogGo.Pool m_pool;

    public class Factory : PlaceholderFactory<Fog> { }


    private Transform m_goParent;
    private Vector2 m_position;
    private int m_sortingOrder;

    private eFogType m_type;

    private bool m_active = false;
    private FogGo m_fogGo = null;

    private bool m_tempFogShow = true;


    public void SetTransformParent(Transform trans)
    {
        m_goParent = trans;
    }

    public void SetPosition(Vector2 position)
    {
        m_position = position;
    }

    public void SetOrder(int order)
    {
        m_sortingOrder = order;
    }

    public void SetFog(eFogType type)
    {
        m_type = type;
    }

    public bool ACTIVE_DISPLAY
    {
        set
        {
            if (m_type == eFogType.nil)    // Ã»ÓÐÎí
                return;

            if (value == m_active)
                return;

            m_active = value;

            if (m_active)
            {
                m_fogGo = m_pool.Spawn();
                m_fogGo.transform.SetParent(m_goParent);
                m_fogGo.transform.localPosition = m_position;
                m_fogGo.SetOrder(m_sortingOrder);
            }
            else
            {
                m_pool.Despawn(m_fogGo);
                m_fogGo = null;
            }
        }
    }


    public void RefreshFog(int x, int y, Vector2 mapPosition, int radius)
    {
        if (m_fogGo == null)
            return;

        if (m_type == eFogType.permanent)
        {
            m_fogGo.gameObject.SetActive((mapPosition - new Vector2(m_fogGo.transform.localPosition.x, m_fogGo.transform.localPosition.y)).magnitude > radius);
        }
        else if (m_type == eFogType.temporary)
        {
            if ((mapPosition - new Vector2(m_fogGo.transform.localPosition.x, m_fogGo.transform.localPosition.y)).magnitude <= radius)
                m_tempFogShow = false;

            m_fogGo.gameObject.SetActive(m_tempFogShow);
        }
    }
}
