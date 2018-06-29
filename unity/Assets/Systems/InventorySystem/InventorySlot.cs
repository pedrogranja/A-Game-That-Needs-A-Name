using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot {
    [SerializeField] private Item item;
    [SerializeField] private int itemCount;


    public InventorySlot()
    {
    }


    public InventorySlot(Item item)
    {
        this.item = item;
    }


    public void setItem(Item item)
    {
        this.item = item;
    }


    public Item getItem()
    {
        return item;
    }


    public void incItemCount()
    {
        itemCount++;
    }


    public void decItemCount()
    {
        itemCount--;
    }


    public void setItemCount(int newItemCount)
    {
        this.itemCount = newItemCount;
    }
}
