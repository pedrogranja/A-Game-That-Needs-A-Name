using System;
using System.Collections.Generic;
using UnityEngine;

//item and time in the current node
public class ItemTuple
{
    public Transform item;
    public float timeInCurrentNode;
    public float timeToSpendInCurrentNode;

    public ItemTuple(Transform item)
    {
        this.item = item;
        this.timeInCurrentNode = 0;
        this.timeToSpendInCurrentNode = 0;
    }

    public ItemTuple(Transform item, float timeInCurrentNode, float timeToSpendInCurrentNode)
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

public abstract class Node : MonoBehaviour {

    [SerializeField] protected Transform targetTransform;
    [SerializeField] protected float throughputItemsPerSec = 1;
    [SerializeField] private int testId;

    [SerializeField] protected List<Node> inputs;
    [SerializeField] protected List<Node> outputs;
    [SerializeField] protected float minTimeBetweenItemsInSecs = 0.20f;

    private static int testIdGenerator = 0;
    [SerializeField] protected bool hasOutput = false;

    [SerializeField] protected List<Transform> testItems;
    protected List<ItemTuple> items;
    ItemTuple outputItem;
    ItemTuple outputItemClone;
    ItemTuple lastReceivedItem;       //variable that keeps track of the position of the last received item in 
                                      //order to see if we can receive a new item according to minTimeBetweenItemsInSecs
    [SerializeField] protected Boolean stopped = false;
    protected Node nextOutput = null;


    void Start()
    {

        items = new List<ItemTuple>();

        if (targetTransform == null)
            targetTransform = this.gameObject.transform;

        testId = testIdGenerator;
        testIdGenerator++;
        nextOutput = getOutputProtocol();
        start();
    }


    public void update()
    {
        this.stopped = false;

        if (nextOutput != null && !nextOutput.canReceiveItem())
        {
            this.stopped = true;
            return;
        }

        if (this.testItems.Count>0)
        {
            if (canReceiveItem() && !inputFull())
            {
                Transform item = this.testItems[0];
                this.testItems.RemoveAt(0);
                ItemTuple itemTuple = new ItemTuple(item);
                acceptItem(itemTuple);
            }
        }

        foreach (ItemTuple itemTuple in this.items)
        {
            if (hasOutput)
                continue;

            processItem(itemTuple);
            itemTuple.timeInCurrentNode += Time.deltaTime;

            if (itemTuple.timeInCurrentNode >= itemTuple.timeToSpendInCurrentNode)
            {
                hasOutput = true;
                outputItem = itemTuple;
            }
        }

        if(lastReceivedItem!=null)
           lastReceivedItem.timeInCurrentNode += Time.deltaTime;

        if (hasOutput)
           itemReachedEnd(outputItem,nextOutput);
    }


    private void itemReachedEnd(ItemTuple item, Node output)
    {
        if (output == null)
            return;

        if (output.inputFull())
            return;

        hasOutput = false;
        itemReachedEndProtocol(item);
        this.items.Remove(item);
        output.acceptItem(item);
        nextOutput = getOutputProtocol();
    }


    public void acceptItem(ItemTuple item)
    {
        if (item == null)
            return;

        item.timeInCurrentNode = 0;
        item.timeToSpendInCurrentNode = 1 / throughputItemsPerSec;
        lastReceivedItem = ItemTuple.clone(item);
        this.items.Insert(0,item);
        acceptItemProtocol(item);
    }


    public bool inputFull()
    {
        if (this.lastReceivedItem == null)
            return false;

        if (this.lastReceivedItem.timeInCurrentNode >= this.minTimeBetweenItemsInSecs)
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

    public void addOutput(Node output)
    {
        if (this.outputs == null)
            this.outputs = new List<Node>();

        this.outputs.Add(output);
    }


    public void addTestItem(Transform item)
    {
        if (this.testItems == null)
            this.testItems = new List<Transform>();

        this.testItems.Add(item);
    }

    //-------------------------------------------------SUBCLASS TEMPLATE----------------------------------------------------//
    //ON SUBCLASS U HAVE TO GUARANTEE THE ITEM GETS TO THE OUTPUT ACCORDING TO THE THROUGHTPUT
    protected abstract void start();

    public abstract void processItem(ItemTuple item);               //specify here what u want the item to do while in the node

    public abstract void acceptItemProtocol(ItemTuple item);        //called when item enters node

    public abstract void itemReachedEndProtocol(ItemTuple item);    //called when item needs to be pushed to next node

    public abstract Node getOutputProtocol();                  //returns the output for the next item

    public List<Node> getInputs() { return this.inputs; }
    public List<Node> getOutputs() { return this.outputs; }
}
