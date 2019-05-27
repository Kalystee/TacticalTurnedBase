using UnityEngine;
using System;

namespace PNN.Characters.Stats
{
    [Serializable]
    public class CharacterStatVariable : CharacterStat
    {
        /// <summary>
        /// Called when the current value is changed
        /// </summary>
        public Action<CharacterStatVariable> onCurrentValueChanged;

        /// <summary>
        /// Current value of the stat
        /// </summary>
        public virtual float CurrentValue
        {
            get
            {
                return _currentValue;
            }
            set
            {
                if (value != _currentValue)
                {
                    _currentValue = Mathf.Clamp(value, 0, Value);
                    onCurrentValueChanged?.Invoke(this);
                }
            }
        }

        protected float _currentValue;

        /// <summary>
        /// Value of the Stat
        /// </summary>
        public override float Value
        {
            get
            {
                if (isDirty)
                {
                    _value = CalculateFinalValue();
                    _currentValue = Mathf.Min(_value, _currentValue);
                    onValueChanged?.Invoke(this);
                    isDirty = false;
                }
                return _value;
            }
        }
        
        public CharacterStatVariable(float maxBaseValue) : base(maxBaseValue)
        {
            this.CurrentValue = maxBaseValue;
        }
    }
}
