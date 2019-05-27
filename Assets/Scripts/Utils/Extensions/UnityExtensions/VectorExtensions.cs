using UnityEngine;

public static class VectorExtensions
{
    public static Vector2Int ExchangeXandY(this Vector2Int vector3)
    {
        return new Vector2Int(vector3.y, vector3.x);
    }

    public static Vector2 ExchangeXandY(this Vector2 vector3)
    {
        return new Vector2(vector3.y, vector3.x);
    }

    public static Vector3Int ExchangeYandZ(this Vector3Int vector3)
    {
        return new Vector3Int(vector3.x, vector3.z, vector3.y);
    }

    public static Vector3Int ExchangeXandZ(this Vector3Int vector3)
    {
        return new Vector3Int(vector3.z, vector3.y, vector3.x);
    }

    public static Vector3Int ExchangeXandY(this Vector3Int vector3)
    {
        return new Vector3Int(vector3.y, vector3.x, vector3.z);
    }

    public static Vector3 ExchangeYandZ(this Vector3 vector3)
    {
        return new Vector3(vector3.x, vector3.z, vector3.y);
    }

    public static Vector3 ExchangeXandZ(this Vector3 vector3)
    {
        return new Vector3(vector3.z, vector3.y, vector3.x);
    }

    public static Vector3 ExchangeXandY(this Vector3 vector3)
    {
        return new Vector3(vector3.y, vector3.x, vector3.z);
    }

    public static Vector2 AsVector2(this Vector2Int vector2Int)
    {
        return new Vector2(vector2Int.x, vector2Int.y);
    }

    public static Vector2 AsVector2(this Vector3Int vector3Int)
    {
        return new Vector2(vector3Int.x, vector3Int.y);
    }

    public static Vector2 AsVector3(this Vector2Int vector2Int, int zValue = 0)
    {
        return new Vector3(vector2Int.x, vector2Int.y, zValue);
    }

    public static Vector2 AsVector3(this Vector3Int vector3Int)
    {
        return new Vector3(vector3Int.x, vector3Int.y, vector3Int.z);
    }

    public static Vector2Int AsVector2Int(this Vector3Int vector3Int)
    {
        return new Vector2Int(vector3Int.x, vector3Int.y);
    }

    public static Vector3Int AsVector3Int(this Vector2Int vector2Int, int zValue = 0)
    {
        return new Vector3Int(vector2Int.x, vector2Int.y, zValue);
    }

    public static Vector2Int AsVector2Int(this Vector2 vector2)
    {
        return new Vector2Int(Mathf.RoundToInt(vector2.x), Mathf.RoundToInt(vector2.y));
    }

    public static Vector3Int AsVector3Int(this Vector3 vector3)
    {
        return new Vector3Int(Mathf.RoundToInt(vector3.x), Mathf.RoundToInt(vector3.y), Mathf.RoundToInt(vector3.z));
    }

    public static Vector2Int AsVector2IntFloor(this Vector2 vector2)
    {
        return new Vector2Int(Mathf.FloorToInt(vector2.x), Mathf.FloorToInt(vector2.y));
    }

    public static Vector3Int AsVector3IntFloor(this Vector3 vector3)
    {
        return new Vector3Int(Mathf.FloorToInt(vector3.x), Mathf.FloorToInt(vector3.y), Mathf.FloorToInt(vector3.z));
    }

    public static Vector2Int AsVector2IntCeil(this Vector2 vector2)
    {
        return new Vector2Int(Mathf.CeilToInt(vector2.x), Mathf.CeilToInt(vector2.y));
    }

    public static Vector3Int AsVector3IntCeil(this Vector3 vector3)
    {
        return new Vector3Int(Mathf.CeilToInt(vector3.x), Mathf.CeilToInt(vector3.y), Mathf.CeilToInt(vector3.z));
    }

    public static int Distance(this Vector2 vector2)
    {
        return Mathf.RoundToInt(Mathf.Abs(vector2.x) + Mathf.Abs(vector2.y));
    }

    public static int Distance(this Vector2Int vector2Int)
    {
        return Mathf.Abs(vector2Int.x) + Mathf.Abs(vector2Int.y);
    }

    public static int Distance(this Vector3 vector3)
    {
        return Mathf.RoundToInt(Mathf.Abs(vector3.x) + Mathf.Abs(vector3.y) + Mathf.Abs(vector3.z));
    }

    public static int Distance(this Vector3Int vector3Int)
    {
        return Mathf.Abs(vector3Int.x) + Mathf.Abs(vector3Int.y) + Mathf.Abs(vector3Int.z);
    }
}