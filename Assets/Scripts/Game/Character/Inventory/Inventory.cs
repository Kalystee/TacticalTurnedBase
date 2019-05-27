using System.Collections.Generic;
using System.Linq;

namespace PNN.Characters.Items
{
    public class Inventory
    {

        /// <summary>
        /// Items of the Inventory
        /// </summary>
        public List<ItemStack> items { get; private set; }

        /// <summary>
        /// Size of the Inventory
        /// </summary>
        public int Size
        {
            get { return this.items.Count; }

        }

        /// <summary>
        /// Property to get the ItemStack of at a certain index
        /// </summary>
        /// <param name="index">Idex of the ItemStack</param>
        /// <returns>ItemStack</returns>
        public ItemStack this[int index]
        {
            get { return this.items[index]; }
        }

        public Inventory()
        {
            this.items = new List<ItemStack>();
        }

        /// <summary>
        /// Method to check how many Item you get in all your inventory
        /// </summary>
        /// <param name="item">Item</param>
        /// <returns>Total ammount of the item</returns>
        public int GetTotalQuantityOfItem(ItemBase item)
        {
            if (item != null)
            {
                return items.Where(i => i.Item == item).Sum(i => i.Quantity);
            }
            return 0;
        }

        /// <summary>
        /// Method to add an item to the inventory
        /// </summary>
        /// <param name="itemS">ItemStack to add</param>
        public void AddItem(params ItemStack[] newItems)
        {
            foreach (ItemStack newItem in newItems)
            {
                ItemStack sameItemIn = this.items.First(it => it.Item == newItem.Item && it.HasFreeSpace());
                //If we don't have to create new ItemStack
                if (newItem.Quantity <= sameItemIn.GetRemainingSpace())
                {
                    sameItemIn.IncreaseQuantity(newItem.Quantity);
                }
                //Else check how many full itemStack must be created and if an itemStack with the remaining quantity have to be created
                else
                {
                    int numberOfFullSack = newItem.Quantity / newItem.Item.StackLimit;
                    int quantityRemaining = newItem.Quantity % newItem.Item.StackLimit;

                    for (int i = 0; i < numberOfFullSack; i++)
                    {
                        this.items.Add(new ItemStack(newItem.Item, newItem.Item.StackLimit));
                    }

                    if (quantityRemaining != 0)
                    {
                        this.items.Add(new ItemStack(newItem.Item, quantityRemaining));
                    }

                }
            }
        }

        /// <summary>
        /// Method to remove a quantity of an Item 
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <param name="quantity">Quantity you wnat to remove</param>
        public void RemoveItem(ItemBase item, int quantity = 1)
        {
            if (this.GetTotalQuantityOfItem(item) >= quantity)
            {
                ItemStack sameItemIn = this.items.Last(i => i.Item == item);
                //If we don't have to delete ItemStack
                if (sameItemIn.Quantity >= quantity)
                {
                    sameItemIn.ReduceQuantity(quantity);
                }
                //Else check how many full itemStack must be remove and if an itemStack must be reduce with the quantity remaining
                else
                {
                    quantity -= sameItemIn.Quantity;
                    this.items.Remove(sameItemIn);

                    int fullStackToDelete = quantity / item.StackLimit;
                    int quantityRemaining = quantity % item.StackLimit;

                    for (int i = 0; i < fullStackToDelete; i++)
                    {
                        this.items.Remove(this.items.Last(l => l.Item == item));
                    }

                    this.items.Last(l => l.Item == item).ReduceQuantity(quantityRemaining);
                }
            }
        }

        /// <summary>
        /// Method to check if the Inventory get the Item wanted
        /// </summary>
        /// <param name="item">item wanted</param>
        /// <returns>True if the inventory get the item wanted</returns>
        public bool HasItem(ItemBase item)
        {
            return this.items.Any(i => i.Item == item);
        }

        /// <summary>
        /// Method to know if two Inventory are equals
        /// </summary>
        /// <param name="obj">Object to compare</param>
        /// <returns>True is they are equals</returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is Inventory)
            {
                Inventory i = obj as Inventory;
                return this.items.Equals(i.items);
            }
            return false;
        }
    }
}