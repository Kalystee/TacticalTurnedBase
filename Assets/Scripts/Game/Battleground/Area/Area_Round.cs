using UnityEngine;
using System.Collections.Generic;

namespace PNN.Battlegrounds.Areas
{
    public class Area_Round : Area
    {
        readonly int radius;

        public Area_Round(int radius)
        {
            this.radius = radius;
        }

        public override Vector2Int[] GetAffectedArea(Vector2Int offset, bool includeCenter)
        {
            List<Vector2Int> listVector = new List<Vector2Int>(/*Mathf.CeilToInt(Mathf.Pow(radius * 2 + 1, 2))*/);

            for (int x = -radius; x <= radius; x++)
            {
                for (int y = -radius; y <= radius; y++)
                {
                    Vector2Int _position = new Vector2Int(x, y);
                    if (_position.magnitude <= radius && (includeCenter || _position != Vector2Int.zero))
                    {
                        listVector.Add(_position + offset);
                    }
                }
            }

            return listVector.ToArray();
        }

        public override bool IsInside(Vector2Int position)
        {
            return position.magnitude <= radius;
        }
    }
}