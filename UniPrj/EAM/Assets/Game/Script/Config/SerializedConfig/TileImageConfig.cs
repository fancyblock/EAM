using System;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class TileImageConfig 
{
    public List<string> m_shapeSequence;
    public List<TileShape> m_normalTile;
    public List<BaseStone> m_baseStone;
}


[Serializable]
public class TileShape
{
    public List<Sprite> m_shape;
}


[Serializable]
public class BaseStone
{
    public List<Sprite> m_baseStone;
}
