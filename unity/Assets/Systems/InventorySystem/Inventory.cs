using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    private LinkedList<InventorySlot> inventorySlots;

    public Inventory(int numSlots)
    {
        for(int i = 0; i < numSlots; i++)
        {
            inventorySlots.AddLast(new InventorySlot());
        }
    }
}
