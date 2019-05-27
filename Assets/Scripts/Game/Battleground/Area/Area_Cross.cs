using UnityEngine;
using System.Collections.Generic;

namespace PNN.Battlegrounds.Areas
{
    public class Area_Cross : Area
    {
        readonly int range;
        readonly CrossType type;

        public Area_Cross(int range, CrossType type)
        {
            this.range = range;
            this.type = type;
        }

        public override Vector2Int[] GetAffectedArea(Vector2Int offset, bool includeCenter)
        {
            List<Vector2Int> listVector = new List<Vector2Int>(range * 8 + 1);
            for (int x = -range; x <= range; x++)
            {
                for (int y = -range; y <= range; y++)
                {
                    Vector2Int position = new Vector2Int(x, y);
                    if (includeCenter || position != Vector2Int.zero)
                    {
                        if (type == CrossType.STRAIGHT)
                        {
                            if (position.x == 0 && Mathf.Abs(position.y) <= range
                                    || position.y == 0 && Mathf.Abs(position.x) <= range)
                            {
                                listVector.Add(position + offset);
                            }
                        }
                        else if (type == CrossType.DIAGONAL)
                        {
                            if (Mathf.Abs(position.x) == Mathf.Abs(position.y) && Mathf.Abs(position.x) <= range)
                            {
                                listVector.Add(position + offset);
                            }
                        }
                        else
                        {
                            if (position.x == 0 && Mathf.Abs(position.y) <= range
                                || position.y == 0 && Mathf.Abs(position.x) <= range
                                || Mathf.Abs(position.x) == Mathf.Abs(position.y) && Mathf.Abs(position.x) <= range)
                            {
                                listVector.Add(position + offset);
                            }
                        }
                    }
                }
            }
            return listVector.ToArray();
        }

        public override bool IsInside(Vector2Int position)
        {
            if (type == CrossType.STRAIGHT)
            {
                    return position.x == 0 && Mathf.Abs(position.y) <= range
                        || position.y == 0 && Mathf.Abs(position.x) <= range;
            }
            else if (type == CrossType.DIAGONAL)
            {
                return Mathf.Abs(position.x) == Mathf.Abs(position.y) && Mathf.Abs(position.x) <= range;
            }
            else
            {
                return position.x == 0 && Mathf.Abs(position.y) <= range
                        || position.y == 0 && Mathf.Abs(position.x) <= range
                        || Mathf.Abs(position.x) == Mathf.Abs(position.y) && Mathf.Abs(position.x) <= range;
            }
        }

        public enum CrossType
        {
            STRAIGHT,
            DIAGONAL,
            BOTH
        }
    }
}