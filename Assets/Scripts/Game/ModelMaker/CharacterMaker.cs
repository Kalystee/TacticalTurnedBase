using System.Collections.Generic;
using UnityEngine;
using System;
using PNN.Characters;
using Sirenix.OdinInspector;

namespace PNN.Makers
{
    [Serializable, InlineProperty]
    public class CharacterMaker
    {
        [SerializeField] private string name;
        [SerializeField] private int health;
        [SerializeField] private int pa;
        [SerializeField] private int strength;
        [SerializeField] private int dexterity;
        [SerializeField] private int constitution;
        [SerializeField] private int intelligence;
        [SerializeField] private List<AbilityBase> abilities;

        public Character CreateCharacter()
        {
            Character character = new Character(name, health, pa, strength, dexterity, constitution, intelligence);
            character.abilityKnowledge.LearnAbilities(abilities.ToArray());
            return character;
        }
    }
}
