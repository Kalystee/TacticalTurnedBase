using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PNN.Characters.Stats
{
    [Serializable]
    public class CharacterStat
    {
        /// <summary>
        /// Called when the base value is changed
        /// </summary>
        public Action<CharacterStat> onBaseValueChanged;

        /// <summary>
        /// Called when the base value is changed
        /// </summary>
        public Action<CharacterStat> onValueChanged;

        /// <summary>
        /// Base Value of the Stat
        /// </summary>
        public virtual float BaseValue
        {
            get
            {
                return _baseValue;
            }
            set
            {
                if (value != _baseValue)
                {
                    _baseValue = value;
                    onBaseValueChanged?.Invoke(this);
                }
            }
        }
        protected float _baseValue;

        /// <summary>
        /// Value of the Stat
        /// </summary>
        public virtual float Value
        {
            get
            {
                if(isDirty)
                {
                    _value = CalculateFinalValue();
                    onValueChanged?.Invoke(this);
                    isDirty = false;
                }
                return _value;
            }
        }

        protected bool isDirty = true;
        protected float _value;

        protected readonly List<StatModifier> statModifiers;
        
        /// <summary>
        /// List of all stat modifiers
        /// </summary>
        public readonly ReadOnlyCollection<StatModifier> StatModifiers;

        public CharacterStat()
        {
            statModifiers = new List<StatModifier>();
            StatModifiers = statModifiers.AsReadOnly();

            this.BaseValue = 0f;
        }

        public CharacterStat(float baseValue) : this()
        {
            this.BaseValue = baseValue;
        }
        /// <summary>
        /// Add a new modifier
        /// </summary>
        /// <param name="mod">Modifier added</param>
        public virtual void AddModifier(StatModifier mod)
        {
            isDirty = true;
            statModifiers.Add(mod);
            statModifiers.Sort(CompareModifierOrder);
        }

        protected virtual int CompareModifierOrder(StatModifier a, StatModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0;
        }

        /// <summary>
        /// Remove a modifier to the stat
        /// </summary>
        /// <param name="mod">Modifier removed</param>
        /// <returns>Has the modifier been removed?</returns>
        public virtual bool RemoveModifier(StatModifier mod)
        {
            if(statModifiers.Remove(mod))
            {
                isDirty = true;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Remove all modifier from a source
        /// </summary>
        /// <param name="source">Source of modifiers</param>
        /// <returns>Has a modifier been removed?</returns>
        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            bool didRemove = false;

            for (int i = statModifiers.Count - 1; i >= 0; i--)
            {
                if (statModifiers[i].Source == source)
                {
                    isDirty = true;
                    didRemove = true;
                    statModifiers.RemoveAt(i);
                }
            }

            return didRemove;
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            for (int i = 0; i < statModifiers.Count; i++)
            {
                StatModifier mod = statModifiers[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if(mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if(i + 1 >= statModifiers.Count || statModifiers[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            return (float)Math.Round(finalValue, 4);
        }
    }
}
