using UnityEngine;

namespace PNN.Battlegrounds.Areas
{
    public class Area_Point : Area
    {
        public Area_Point()
        {

        }

        public override Vector2Int[] GetAffectedArea(Vector2Int offset, bool includedCenter)
        {
            if (includedCenter)
                return new Vector2Int[1] { Vector2Int.zero + offset };
            else
                return new Vector2Int[0];
        }

        public override bool IsInside(Vector2Int position)
        {
            return position == Vector2Int.zero;
        }
    }
}