using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    [SerializeField] public GameObject gameObject;
    [SerializeField] public Sprite itemIcon;

    public Item() {}
    public Item(GameObject gameObject, Sprite itemIcon) {
        this.gameObject = gameObject;
        this.itemIcon = itemIcon;
    }
}
