using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for handling the press of a building block button in editor mode
public class BottomInventoryHandler : MonoBehaviour {

    ObjectCreator objectCreator;
    [SerializeField] GameObject item;

    // Use this for initialization
    void Start () {
        objectCreator = ObjectCreator.getInstance();
	}
	
    public void inventoryClicked()
    {
        objectCreator.setObjectToInstanciate(item);
    }
}
