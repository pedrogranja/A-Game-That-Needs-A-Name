using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public interface AddRemoveItemInterface
    {
        // The return of this function must be the Item you want to add to the gameObject,
        // or null in case you dont want to add anything
        Item readyToAddItem();

        // Called when the received item is ready to be removed from the network,
        // return true if you want to remove the item from the network or false otherwise
        bool readyToRemoveItem(Item item);
    }
}
