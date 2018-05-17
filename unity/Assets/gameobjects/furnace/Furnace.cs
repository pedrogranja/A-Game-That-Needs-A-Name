using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furnace : Node
{
    [SerializeField] private Transform objectToInstantiate;

    protected override void start()
    {
    }

    public override void acceptItemProtocol(ItemTuple item)
    {
        Destroy(item.item.gameObject);
        item.item.Rotate(new Vector3(90,0,0));
    }

    public override Node getOutputProtocol()
    {
        if (this.outputs.Count == 0)
            return null;

        return this.outputs[0];
    }

    public override void itemReachedEndProtocol(ItemTuple item)
    {
        item.item = Instantiate(objectToInstantiate);
    }

    public override void processItem(ItemTuple item)
    {
    }
}
