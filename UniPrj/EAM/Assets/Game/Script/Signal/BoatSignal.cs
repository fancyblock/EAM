using UnityEngine;


public class SignalInitBoat : BaseSignal { }

public class SignalBoatPositionChange : BaseSignal
{
    public int X;
    public int Y;

    public Vector2 MapPosition;
}
