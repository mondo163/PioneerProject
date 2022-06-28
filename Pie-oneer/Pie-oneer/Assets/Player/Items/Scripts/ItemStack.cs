using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ItemStack
{
    private const int MAX_SIZE = 10; 

    public IItem Item { get; set; }
    public int Amount { get; set; }

    public bool IsFull() => Amount >= MAX_SIZE ? true : false;
    public bool IsEmpty() => Amount <= 0 ? true : false;

    public ItemStack()
    {
        Amount = 0;
    }
}

public class InventoryEventArgs : EventArgs
{
    public InventoryEventArgs(ItemStack itemStack, int slot)
    {
        this.itemStack = itemStack;
        this.slot = slot;
    }

    public ItemStack itemStack;
    public int slot;
}

public class ItemEventArgs : EventArgs
{
    public ItemEventArgs(IItem item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }

    public IItem item;
    public int amount;
}

