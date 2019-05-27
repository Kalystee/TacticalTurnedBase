using PNN.Battlegrounds;
using UnityEngine;

public class Battleground_OnTileChangedArgs
{
    public readonly Vector2Int position;
    public readonly BattlegroundTile newTile;
    public readonly bool wasNull;

    public Battleground_OnTileChangedArgs(Vector2Int position, BattlegroundTile newTile, bool wasNull)
    {
        this.position = position;
        this.newTile = newTile;
        this.wasNull = wasNull;
    }
}
