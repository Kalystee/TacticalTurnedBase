namespace PNN.Characters.Items
{
    public class Gear
    {
        public Weapon Weapon { get; set; }

        public Headset Headset { get; set; }

        public ChestPlate ChestPlate { get; set; }

        public Gloves Gloves { get; set; }

        public Boots Boots { get; set; }

        public Gear()
        {

        }

        public Gear(Weapon weapon, Headset headset, ChestPlate chestPlate, Gloves gloves, Boots boots)
        {
            this.Weapon = weapon;
            this.Headset = headset;
            this.ChestPlate = chestPlate;
            this.Gloves = gloves;
            this.Boots = boots;
        }

    }
}

