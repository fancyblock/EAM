using System.Collections;
using System.Collections.Generic;
using System;


[Serializable]
public enum eFogType
{
    nil,
    permanent,
    temporary,
}


[Serializable]
public enum eTerrain
{
    nil,
    ground,
    block,
}


[Serializable]
public enum eTileShape : int
{
    center = 0,
    rightCorner,
    rightInCorner,
    leftCorner,
    leftInCorner,
    upCorner,
    upInCorner,
    downCorner,
    downInCorner,
    edgeUpRight,
    edgeUpLeft,
    edgeDownRight,
    edgeDownLeft,
}
