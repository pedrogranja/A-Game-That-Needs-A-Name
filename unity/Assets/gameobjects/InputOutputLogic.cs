using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InputOutputLogic : MonoBehaviour {

    [SerializeField] protected Transform input;
    protected InputOutputLogic inputScript;

    [SerializeField] protected Transform output;
    protected InputOutputLogic outputScript;

    public Transform outputItem;

    [SerializeField] private List<Transform> inputItems; //FOR DEBUGGING

    // Use this for initialization
    public void init () {
        if (input != null)
        {
            inputScript = input.GetComponent<InputOutputLogic>();
        }
        if (output != null)
            outputScript = output.GetComponent<InputOutputLogic>();
    }
	
	// Update is called once per frame
	void Update () {

        if (inputReady())
            treatInput();

        update();
        setupOutput();

    }


    public abstract void treatInput();
    public abstract void update();
    public abstract void setupOutput();


    public bool inputReady()
    {
        if(inputScript != null)
        {
            if (inputScript.outputItem != null)
                return true;
            else
                return false;
        }

        //for debugging
        else if(inputItems.Count > 0)
        {
            return true;
        }

        return false;
    }


    public Transform getInputItem()
    {

        Transform item = null;

        if (inputScript != null)
        {
            item = inputScript.outputItem;
            inputScript.outputItem = null;
        }

        //DEBUGGING
        if(item == null)
        {
            item = inputItems[0];
            inputItems.RemoveAt(0);
        }

        return item;
    }

}
