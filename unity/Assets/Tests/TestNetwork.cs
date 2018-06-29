using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNetwork : MonoBehaviour, AddRemoveItemInterface
{

    [SerializeField] GameObject conveyorBelt;
    [SerializeField] GameObject furnace;
    [SerializeField] GameObject item;
    [SerializeField] private int numBelts;
    [SerializeField] private int numItems;
    [SerializeField] private Vector3 position;

    public Item readyToAddItem()
    {
        return new Item((GameObject)Instantiate(item, new Vector3(0, 0, 0), Quaternion.AngleAxis(-90, Vector3.forward)), null);
    }

    public bool readyToRemoveItem(Item item)
    {
        return false;
    }

    // Use this for initialization
    void Start()
    {
        position = this.transform.position -= (new Vector3(0, 0, 0));

        GameObject prev = (GameObject)Instantiate(conveyorBelt, position, Quaternion.AngleAxis(-90, Vector3.forward));
        prev.transform.Rotate(new Vector3(0, -90, 0));
        position += new Vector3(1.722f, 0, 0);

        ItemTransportationService.callWhenReadyToAddItem(prev,this);

        for (int i = 0; i < numBelts / 2; i++)
        {
            //create new instance
            GameObject instance = (GameObject)Instantiate(conveyorBelt, position, Quaternion.AngleAxis(-90, Vector3.forward));
            instance.transform.Rotate(new Vector3(0, -90, 0));
            position += new Vector3(1.72f, 0, 0);

            //connect it to prev
            ItemTransportationService.addOutputToNode(instance,prev);
            prev = instance;
        }

        //create new instance
        GameObject instance2 = (GameObject)Instantiate(furnace, position, Quaternion.AngleAxis(-90, Vector3.forward));
        instance2.transform.Rotate(new Vector3(0, -90, 0));
        position += new Vector3(1.72f, 0, 0);
        ItemTransportationService.addOutputToNode(instance2, prev);
        prev = instance2;

        for (int i = 0; i < numBelts / 2; i++)
        {
            //create new instance
            GameObject instance = (GameObject)Instantiate(conveyorBelt, position, Quaternion.AngleAxis(-90, Vector3.forward));
            instance.transform.Rotate(new Vector3(0, -90, 0));
            position += new Vector3(1.72f, 0, 0);

            //connect it to prev
            ItemTransportationService.addOutputToNode(instance, prev);
            prev = instance;
        }
    }
}
