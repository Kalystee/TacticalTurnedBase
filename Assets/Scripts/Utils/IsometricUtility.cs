using UnityEngine;

public static class IsometricUtility
{
    public static Vector2 CarthesianToIsometric(this Vector2 position)
    {
        return new Vector2((position.x - position.y), (position.y + position.x) * 0.5f) * 0.5f;
    }

    public static Vector3 CarthesianToIsometric(this Vector3 position)
    {
        return new Vector3((position.x - position.y), (position.y + position.x) * 0.5f) * 0.5f;
    }

    public static Vector2 IsometricToCarthesian(this Vector2 position)
    {
        return new Vector2((2f * position.y + position.x) * 0.5f, (2f * position.y - position.x) * 0.5f) * 2f;
    }

    public static Vector3 IsometricToCarthesian(this Vector3 position)
    {
        return new Vector3((2f * position.y + position.x) * 0.5f, (2f * position.y - position.x) * 0.5f) * 2f;
    }
}