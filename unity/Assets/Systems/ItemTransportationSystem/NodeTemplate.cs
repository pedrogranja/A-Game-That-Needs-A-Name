using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemTransportation
{
    public abstract class NodeTemplate : MonoBehaviour
    {
        [SerializeField] protected float throughputItemsPerSec = 1;
        [SerializeField] protected float minTimeBetweenItemsInSecs = 0.20f;
        [SerializeField] protected Transform targetTransform;

        [SerializeField] protected List<Node> inputs = new List<Node>();
        [SerializeField] protected List<Node> outputs = new List<Node>();

        public abstract void start();                                //the beggining of the next opperation
        public abstract void processItem(ItemTuple item);            //specify here what u want the item to do while in the node
        public abstract void acceptItemProtocol(ItemTuple item);     //called when item enters node
        public abstract void itemReachedEndProtocol(ItemTuple item); //called when item needs to be pushed to next node
        public abstract Node getOutputProtocol();                    //returns the output for the next item


        //Utility Methods----------------------------------------------------------------------------------------------------------------
        public float getThroughput() { return throughputItemsPerSec; }
        public float getMinTimeBetweenItemsInSecs() { return minTimeBetweenItemsInSecs; }

        public List<Node> getInputs() { return this.inputs; }
        public List<Node> getOutputs() { return this.outputs; }

        public void addOutput(Node output) { this.outputs.Add(output); }
    }
}
