using System;
using System.Text;
using UnityEngine;

namespace PNN.Characters.Stats
{ 
    [Serializable]
    public struct StatModifierObject
    {
        public string name { get { return ToString(); } }

        [SerializeField]
        private float Value;
        [SerializeField]
        private StatModType Type;
        [SerializeField]
        private bool OverrideOrder;
        [SerializeField, Tooltip("Classic Order (Low value comes first) : Flat (100), PercentAdd(200), PercentMulti(300)")]
        private int CustomOrder;

        public StatModifier CreateStatModifier(object source)
        {
            if (OverrideOrder)
                return new StatModifier(Value, Type, CustomOrder, source);
            else
                return new StatModifier(Value, Type, source);
        }

        public override string ToString()
        {
            if(Type == StatModType.Flat)
            {
                StringBuilder builder = new StringBuilder("+ ");
                builder.Append(Math.Round(Value, 2));
                return builder.ToString();
            }
            else if(Type == StatModType.PercentAdd)
            {
                StringBuilder builder = new StringBuilder("+ ");
                builder.Append(Math.Round(Value, 2));
                builder.Append(" %");
                return builder.ToString();
            }
            else
            {
                StringBuilder builder = new StringBuilder("x ");
                builder.Append(Math.Round(Value, 2));
                builder.Append(" %");
                return builder.ToString();
            }
        }
    }
}
