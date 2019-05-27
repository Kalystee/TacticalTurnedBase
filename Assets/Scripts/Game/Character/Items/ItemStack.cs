namespace PNN.Characters.Items
{
    public class ItemStack
    {
        /// <summary>
        /// The Item stacked
        /// </summary>
        public ItemBase Item { get; set; }

        /// <summary>
        /// Quantity of Item
        /// </summary>
        public int Quantity { get; set; }


        public ItemStack(ItemBase item, int quantity = 1)
        {
            this.Item = item;
            this.Quantity = quantity;
        }

        /// <summary>
        /// Method to get the number of item remaining to complete the stack
        /// </summary>
        /// <returns>Number of space remaining</returns>
        public int GetRemainingSpace()
        {
            return this.Item.StackLimit - this.Quantity;
        }

        /// <summary>
        /// Method to check if the stack has free space
        /// </summary>
        /// <returns>True if the stack has free space</returns>
        public bool HasFreeSpace()
        {
            return this.GetRemainingSpace() > 0;
        }

        /*------------------------------------------
                   Method IsValid                   
        *------------------------------------------ */

        /// <summary>
        /// Method to add a quantity of Item of the ItemStack
        /// </summary>
        /// <param name="quantity">Quantity of item to add</param>
        /// <returns>True if the quantity has been updated</returns>
        public bool IncreaseQuantity(int quantity = 1)
        {
            if (this.HasFreeSpace() && this.GetRemainingSpace() >= quantity)
            {
                this.Quantity += quantity;
                return true;
            }
            return false;

        }

        /// <summary>
        /// Method to reduce the quantity of Item of the ItemStack
        /// </summary>
        /// <param name="quantity">Quantity of item to reduce</param>
        public void ReduceQuantity(int quantity = 1)
        {
            this.Quantity -= quantity;
        }
    }
}

