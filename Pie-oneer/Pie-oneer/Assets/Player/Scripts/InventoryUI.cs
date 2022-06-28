using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;

    private Image[] itemSlots;
    private Image[] borderSlots;
    private TextMeshProUGUI[] itemAmounts;

    // Start is called before the first frame update
    void Start()
    {
        inventory.ItemAdded += InventoryItemAdded;
        inventory.ItemRemoved += InventoryItemRemoved;
        inventory.ItemSelected += InventoryItemSelected;
        inventory.ItemUpdated += InventoryItemUpdated;

        itemSlots = gameObject.transform.Find("Slot Items").GetComponentsInChildren<Image>();
        borderSlots = gameObject.transform.Find("Inventory Slots").GetComponentsInChildren<Image>();
        itemAmounts = gameObject.transform.Find("Slot Items").GetComponentsInChildren<TextMeshProUGUI>();

        for (int i = 0; i < itemAmounts.Length; i++)
        {
            itemAmounts[i].enabled = false;
        }
    }

    private void InventoryItemAdded(object sender, InventoryEventArgs args)
    {
        itemSlots[args.slot].sprite = args.itemStack.Item.Sprite;
        itemAmounts[args.slot].enabled = true;
        itemAmounts[args.slot].text = $"{args.itemStack.Amount}";
    }

    private void InventoryItemRemoved(object sender, InventoryEventArgs args)
    {
        if (!args.itemStack.IsEmpty())
        {
            itemAmounts[args.slot].text = $"{args.itemStack.Amount}";
        } 
        else
        {
            itemSlots[args.slot].sprite = null;
            itemAmounts[args.slot].enabled = false;
        }
    }

    private void InventoryItemSelected(object sender, InventoryEventArgs args)
    {
        // Reset color for each item
        foreach (Image border in borderSlots)
        {
            border.color = Color.white;
        }

        // Set the color back for the newly selected item
        borderSlots[args.slot].color = Color.gray;
    }

    private void InventoryItemUpdated(object sender, InventoryEventArgs args)
    {
        itemAmounts[args.slot].text = $"{args.itemStack.Amount}";
    }
}
