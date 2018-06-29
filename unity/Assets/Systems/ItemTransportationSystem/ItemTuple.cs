using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemTransportation
{
    //item and time in the current node
    public class ItemTuple
    {
        public Item item;
        public float timeInCurrentNode;
        public float timeToSpendInCurrentNode;

        public ItemTuple(Item item)
        {
            this.item = item;
            this.timeInCurrentNode = 0;
            this.timeToSpendInCurrentNode = 0;
        }

        public ItemTuple(Item item, float timeInCurrentNode, float timeToSpendInCurrentNode)
        {
            this.item = item;
            this.timeInCurrentNode = timeInCurrentNode;
            this.timeToSpendInCurrentNode = timeToSpendInCurrentNode;
        }

        public static ItemTuple clone(ItemTuple original)
        {
            return new ItemTuple(original.item, original.timeInCurrentNode, original.timeToSpendInCurrentNode);
        }
    }
}