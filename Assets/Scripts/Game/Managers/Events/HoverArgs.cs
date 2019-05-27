using UnityEngine;

public class HoverArgs
{
    public readonly Vector3Int hoveredPosition;

    public HoverArgs(Vector3Int hoveredPosition)
    {
        this.hoveredPosition = hoveredPosition;
    }
}
