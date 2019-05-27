using System.Collections.Generic;
using UnityEngine;

namespace PNN.Battlegrounds.Areas
{
    public class Area_Distance : Area
    {
        readonly int radius;

        public Area_Distance(int radius)
        {
            this.radius = radius;
        }

        public override Vector2Int[] GetAffectedArea(Vector2Int offset, bool includeCenter)
        {
            List<Vector2Int> listVector = new List<Vector2Int>(Mathf.RoundToInt((Mathf.Pow(radius * 2 + 1, 2) - 1) / 2));

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int _position = new Vector2Int(x, y);
                    if ((Mathf.Abs(_position.x) + Mathf.Abs(_position.y)) <= radius && (includeCenter || _position != Vector2Int.zero))
                    {
                        listVector.Add(_position + offset);
                    }
                }
            }

            return listVector.ToArray();
        }

        public override bool IsInside(Vector2Int position)
        {
            return (Mathf.Abs(position.x) + Mathf.Abs(position.y)) <= radius;
        }
    }
}
