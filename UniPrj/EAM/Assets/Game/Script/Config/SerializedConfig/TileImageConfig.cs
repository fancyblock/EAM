using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TileImageConfig 
{
    public List<eTileShape> m_shapeSequence;
    public List<TileShape> m_normalTile;
}


[Serializable]
public class TileShape
{
    public List<Sprite> m_shape;
}
