using Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestItems : MonoBehaviour, AddRemoveItemInterface
{
    [SerializeField] GameObject toInstance;
    [SerializeField] GameObject objectToFillWithItems;

    void Start()
    {
        ItemTransportationService.callWhenReadyToAddItem(objectToFillWithItems,this);
        ItemTransportationService.callWhenReadyToRemoveItem(objectToFillWithItems, this);
    }

    public Item readyToAddItem()
    {
        return new Item((GameObject)Instantiate(toInstance, new Vector3(0,0,0), Quaternion.AngleAxis(-90, Vector3.forward)),null);
    }

    public bool readyToRemoveItem(Item item)
    {
        Destroy(item.gameObject);
        return true;
    }
}
