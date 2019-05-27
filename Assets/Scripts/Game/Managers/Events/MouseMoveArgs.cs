using UnityEngine;

public class MouseMoveArgs
{
    public readonly Vector3 mousePosition;
    public readonly Vector3 oldMousePosition;
    public readonly Vector3Int worldIsometricPosition;
    public readonly Vector3Int oldWorldIsometricPosition;

    public MouseMoveArgs(Vector3 mousePosition, Vector3 oldMousePosition, Vector3Int worldIsometricPosition, Vector3Int oldWorldIsometricPosition)
    {
        this.mousePosition = mousePosition;
        this.oldMousePosition = oldMousePosition;
        this.worldIsometricPosition = worldIsometricPosition;
        this.oldWorldIsometricPosition = oldWorldIsometricPosition;
    }

    public bool IsometricChanged
    {
        get
        {
            return worldIsometricPosition != oldWorldIsometricPosition;
        }
    }
}