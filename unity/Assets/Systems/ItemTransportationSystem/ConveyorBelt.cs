using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemTransportation
{
    public class ConveyorBelt : NodeTemplate
    {

        [SerializeField] private float laneInitialPosition = -0.861f;
        [SerializeField] private float laneFinalPosition = 0.861f;
        private float speed;
        private Vector3 itemInitialPosition;

        public override void start()
        {
            itemInitialPosition = new Vector3(0, laneInitialPosition, 0.2f);
            speed = (laneFinalPosition - laneInitialPosition) * this.throughputItemsPerSec;
        }

        public override void acceptItemProtocol(ItemTuple item)
        {
            item.item.gameObject.transform.SetParent(transform);
            item.item.gameObject.transform.rotation = Quaternion.identity;
            item.item.gameObject.transform.localPosition = itemInitialPosition;
        }

        public override Node getOutputProtocol()
        {
            if (this.outputs.Count == 0)
                return null;

            return this.outputs[0];
        }

        public override void itemReachedEndProtocol(ItemTuple item)
        {
        }

        public override void processItem(ItemTuple item)
        {
            item.item.gameObject.transform.localPosition += new Vector3(0, speed * Time.deltaTime, 0);

            if (this.GetComponents<Node>().Length > 0)
                targetTransform.GetComponent<Renderer>().materials[2].SetTextureOffset("_MainTex", new Vector3((speed / (laneFinalPosition - laneInitialPosition)) / this.GetComponents<Node>().Length * Time.time, 0, 0));
            else
                targetTransform.GetComponent<Renderer>().materials[2].SetTextureOffset("_MainTex", new Vector3((speed / (laneFinalPosition - laneInitialPosition)) * Time.time, 0, 0));
        }

    }
}
