using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private ItemStack[] inventory;
    private int selectedItemStack;
    private int coins;

    // Setup events for when an item is added or removed from the inventory
    public event EventHandler<InventoryEventArgs> ItemAdded;
    public event EventHandler<InventoryEventArgs> ItemRemoved;
    public event EventHandler<InventoryEventArgs> ItemSelected;
    public event EventHandler<InventoryEventArgs> ItemUpdated;
    public event EventHandler<CoinEventArgs> CoinsUpdated;

    public int Slots { get; private set; }
    public int Coins
    {
        get { return coins; }
        set
        {
            // Make sure the total amonunt of coins can never be a negative value.
            if (value < 0)
                throw new Exception("Coin balance cannot be below zero.");

            coins = value;
            CoinsUpdated?.Invoke(this, new CoinEventArgs(coins));
        }
    }

    private void Start()
    {
        Slots = 6;
        coins = 0;
        selectedItemStack = 0;
        inventory = new ItemStack[Slots];

        for (int i = 0; i < Slots; i++)
        {
            inventory[i] = new ItemStack();
        }
    }

    private void Update()
    {
        SelectItemStack();
        UseItem();
    }

    private void SelectItemStack()
    {
        if (Input.GetKeyDown("1"))
            selectedItemStack = 0;
        else if (Input.GetKeyDown("2"))
            selectedItemStack = 1;
        else if (Input.GetKeyDown("3"))
            selectedItemStack = 2;
        else if (Input.GetKeyDown("4"))
            selectedItemStack = 3;
        else if (Input.GetKeyDown("5"))
            selectedItemStack = 4;
        else if (Input.GetKeyDown("6"))
            selectedItemStack = 5;

        if (inventory[selectedItemStack] != null)
            ItemSelected?.Invoke(this, new InventoryEventArgs(inventory[selectedItemStack], selectedItemStack));
    }

    private void UseItem()
    {
        ItemStack itemToUse = inventory[selectedItemStack];

        if (itemToUse != null)
        {
            if (Input.GetKeyDown("e") && !itemToUse.IsEmpty())
            {
                itemToUse.Item.Use();
                RemoveItem(itemToUse);
            }
        }
    }

    public void AddItem(IItem item)
    {
        if (item.Name != "Coin")
        {
            // Go through each slot and check to see if it is either empty or already contains an item.
            for (int i = 0; i < Slots; i++)
            {
                // If null set the slot to an item type.
                // TODO: If the item stack is full add a another item stack of the same item type... Will break other things
                if (inventory[i].IsEmpty())
                {
                    inventory[i].Item = item;
                    inventory[i].Amount++;
                    ItemAdded?.Invoke(this, new InventoryEventArgs(inventory[i], i));
                    break;
                }
                // When the item type is found in the inventory, add onto the item stack unless it's full.
                else if (inventory[i].Item.Name == item.Name && !inventory[i].IsFull())
                {
                    inventory[i].Amount++;
                    ItemUpdated?.Invoke(this, new InventoryEventArgs(inventory[i], i));
                    break;
                }
            }
        }
        else
        {
            // TODO: If different coin values are needed, this section will need to be updated.
            Coins += 1;
        }
    }

    public void RemoveItem(ItemStack itemStack)
    {
        for (int i = 0; i < Slots; i++)
        {
            if (inventory[i] == itemStack)
            {
                itemStack.Amount--;
                if (inventory[i].IsEmpty())
                {
                    inventory[i].Item = null;
                    ItemRemoved?.Invoke(this, new InventoryEventArgs(itemStack, i));
                } else
                {
                    ItemRemoved?.Invoke(this, new InventoryEventArgs(itemStack, i));
                }
                break;
            } 
        }
    }
}