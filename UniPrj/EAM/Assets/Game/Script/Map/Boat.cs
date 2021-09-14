using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Boat : MonoBehaviour
{
    [Inject]
    private MapController m_mapController;

    private float m_velocity;
    private Vector2Int m_destTile;
    private bool m_moving = false;
    private Vector2 m_movingDir;
    private float m_movingDistance;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(m_moving)
        {
            Vector2 step = m_movingDir * m_velocity * Time.deltaTime;

            transform.localPosition = transform.localPosition + new Vector3(step.x, step.y, 0);
            m_movingDistance -= step.magnitude;

            if (m_movingDistance <= 0)
                m_moving = false;
        }
    }

    public void SetPosition(int x, int y)
    {
        transform.localPosition = m_mapController.Tile2Position(x, y);
    }

    public void SetVelocity(float velocity)
    {
        m_velocity = velocity;
    }

    public void MoveTo(int x, int y)
    {
        m_destTile = new Vector2Int(x, y);

        Vector2 dir = (m_mapController.Tile2Position(x, y) - new Vector2(transform.localPosition.x, transform.localPosition.y));
        m_movingDistance = dir.magnitude;
        m_movingDir = dir.normalized;
        m_moving = true;
    }
}
