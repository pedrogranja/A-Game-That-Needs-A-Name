using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemTransportation
{
    public class Furnace : NodeTemplate
    {
        [SerializeField] private Transform objectToInstantiate;

        public override void start()
        {
        }

        public override void acceptItemProtocol(ItemTuple item)
        {
            Destroy(item.item.gameObject);
            item.item.gameObject.transform.Rotate(new Vector3(90, 0, 0));
        }

        public override Node getOutputProtocol()
        {
            if (this.outputs.Count == 0)
                return null;

            return this.outputs[0];
        }

        public override void itemReachedEndProtocol(ItemTuple item)
        {
            item.item.gameObject = Instantiate(objectToInstantiate).gameObject;
        }

        public override void processItem(ItemTuple item)
        {
        }
    }
}
