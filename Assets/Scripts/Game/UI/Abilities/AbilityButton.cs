using UnityEngine;
using UnityEngine.UI;

namespace PNN.UI.Abilities
{
    public class AbilityButton
    {
        public readonly AbilityBase ability;
        public readonly GameObject parent;
        public readonly Image image;
        public readonly Button button;

        public AbilityButton(AbilityBase ability, GameObject parent, Image image, Button button)
        {
            this.ability = ability;
            this.parent = parent;
            this.image = image;
            this.button = button;
        }
    }
}