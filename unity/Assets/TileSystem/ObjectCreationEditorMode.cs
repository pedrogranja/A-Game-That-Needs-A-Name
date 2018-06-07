using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCreationEditorMode : MonoBehaviour {

    [SerializeField] private GameObject objectToInstanciate;

    private static ObjectCreationEditorMode reference;

    void Start()
    {
        reference = this;
    }


    private ObjectCreationEditorMode()
    {
        //we want this to be a singleton
    }


    public static ObjectCreationEditorMode getInstance()
    {
        return reference;
    }


    public void OnTouchDown(GameObject target)
    {
        if (target.GetComponent<MeshRenderer>() == null) //if invisible tile replace by new tile
        {
            createObject(target.transform, objectToInstanciate.transform, new Vector3(0.0f, 0.0f, 0.0f));
            Destroy(target);
        }
        else if(target.GetComponent<HexagonalTile>() != null && target.GetComponent<HexagonalTile>().neighbours.up != null)
        {
            //if there is something on top of the tile than do nothing
        }
        else //if tile is not invisible place new tile on top of old tile
        {
            createObject(target.transform, objectToInstanciate.transform, new Vector3(0.0f, 1.0f, 0.0f));
        }
    }


    //creates an instance of the toInstance tile and places it according to the targetBlock whit a specific offset
    private void createObject(Transform targetBlock, Transform toInstance, Vector3 offset)
    {
        Instantiate(toInstance, targetBlock.position + offset, toInstance.rotation);
    }
}
