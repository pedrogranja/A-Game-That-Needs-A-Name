using System;
using System.Collections.Generic;
using UnityEngine;
using Services;
using System.Linq;

namespace ItemTransportation
{
    public class Node : MonoBehaviour
    {
        [SerializeField] private int testId;

        private static int testIdGenerator = 0;
        [SerializeField] protected bool hasOutput = false;

        [SerializeField] protected List<Transform> testItems;
        protected List<ItemTuple> items;

        [SerializeField] protected Boolean stopped = false;
        [SerializeField] protected NodeTemplate nodeTemplate;
        protected Node nextOutput = null;

        protected List<AddRemoveItemInterface> notifyInAcceptReady = new List<AddRemoveItemInterface>();
        protected List<AddRemoveItemInterface> notifyInOutputReady = new List<AddRemoveItemInterface>();

        void Start()
        {
            NetworkManager.getInstance().addInput(transform);
            items = new List<ItemTuple>();

            testId = testIdGenerator;
            testIdGenerator++;
            nextOutput = nodeTemplate.getOutputProtocol();
            nodeTemplate.start();
        }


        public void update()
        {
            this.stopped = false;

            //if output node stopped than stop this as well
            if (nextOutput != null && !nextOutput.canReceiveItem())
            {
                this.stopped = true;
                return;
            }

            addInput();

            foreach (ItemTuple itemTuple in this.items)
            {
                if (hasOutput)
                    continue;

                nodeTemplate.processItem(itemTuple);
                itemTuple.timeInCurrentNode += Time.deltaTime;

                if (itemTuple.timeInCurrentNode >= itemTuple.timeToSpendInCurrentNode)
                {
                    hasOutput = true;
                }
            }

            if (hasOutput)
                itemReachedEnd(this.items.First(), nextOutput);
        }


        private void itemReachedEnd(ItemTuple item, Node output)
        {
            foreach (AddRemoveItemInterface i in notifyInOutputReady)
            {
                bool toRemove = i.readyToRemoveItem(item.item);

                if (toRemove)
                {
                    this.items.Remove(item);
                    hasOutput = false;
                    return;
                }
            }

            if (output == null)
                return;

            if (output.inputFull())
                return;

            hasOutput = false;
            nodeTemplate.itemReachedEndProtocol(item);
            this.items.Remove(item);
            output.acceptItem(item);
            nextOutput = nodeTemplate.getOutputProtocol();
        }


        public void acceptItem(ItemTuple item)
        {
            if (item == null)
                return;

            item.timeInCurrentNode = item.timeInCurrentNode - item.timeToSpendInCurrentNode;
            item.timeToSpendInCurrentNode = 1 / nodeTemplate.getThroughput();
            this.items.Add(item);
            nodeTemplate.acceptItemProtocol(item);
        }


        public bool inputFull()
        {
            if (items.Count == 0)
                return false;

            if (this.items.Last().timeInCurrentNode >= nodeTemplate.getMinTimeBetweenItemsInSecs())
                return false;

            return true;
        }


        public bool canReceiveItem()
        {
            if (stopped)
                return false;

            if (hasOutput)
                return false;

            return true;
        }

        //--------------------------------------------------Utility Methods-------------------------------------------------------//

        private void addInput()
        {

            if (!canReceiveItem() || inputFull())
            {
                return;
            }

            Item item = null;

            foreach (AddRemoveItemInterface i in notifyInAcceptReady)
            {
                item = i.readyToAddItem();

                if (item != null)
                    break;
            }

            if (item == null)
                return;

            ItemTuple itemTuple = new ItemTuple(item);
            acceptItem(itemTuple);            
        }

        public void addOutput(Node node) { nodeTemplate.getOutputs().Add(node); }

        public List<Node> getOutputs() { return nodeTemplate.getOutputs(); }

        public void addToNotifyOnAcceptReady(AddRemoveItemInterface toNotify)
        {
            this.notifyInAcceptReady.Add(toNotify);
        }

        public void addToNotifyOnOutputReady(AddRemoveItemInterface toNotify)
        {
            this.notifyInOutputReady.Add(toNotify);
        }
    }
}
