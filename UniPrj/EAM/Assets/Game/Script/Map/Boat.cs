using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class Boat : MonoBehaviour
{
    [Inject]
    private MapController m_mapController;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(int x, int y)
    {
        transform.localPosition = m_mapController.Tile2Position(x, y);
    }
}
