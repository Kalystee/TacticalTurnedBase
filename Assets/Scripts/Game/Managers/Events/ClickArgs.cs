using PNN.Enums;
using UnityEngine;

public class ClickArgs
{
    public readonly Vector3Int clickPosition;
    public readonly MouseButton mouseClick;

    public ClickArgs(Vector3Int clickPosition, int mouseClick)
    {
        this.clickPosition = clickPosition;
        this.mouseClick = (MouseButton)mouseClick;
    }

    public ClickArgs(Vector3Int clickPosition, MouseButton mouseClick)
    {
        this.clickPosition = clickPosition;
        this.mouseClick = mouseClick;
    }
}
