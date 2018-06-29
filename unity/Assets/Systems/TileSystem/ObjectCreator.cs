using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsable for instancing new tiles
public class ObjectCreator : MonoBehaviour {

    [SerializeField] private GameObject objectToInstanciate;

    private static ObjectCreator reference;

    void Start()
    {
        reference = this;
    }


    private ObjectCreator()
    {
        //we want this to be a singleton
    }


    public static ObjectCreator getInstance()
    {
        return reference;
    }


    //creates an instance of the toInstance tile and places it according to the targetBlock whit a specific offset
    private void createObject(Transform targetBlock, Transform toInstance, Vector3 offset)
    {
        Instantiate(toInstance, targetBlock.position + offset, toInstance.rotation);
    }

    
    public void setObjectToInstanciate(GameObject newObject)
    {
        this.objectToInstanciate = newObject;
    }

    public GameObject getObjectToInstanciate()
    {
        return this.objectToInstanciate;
    }


    //called when tile is clicked
    public void OnTouchDown(GameObject target)
    {
        if (target.GetComponent<MeshRenderer>() == null) //if invisible tile replace by new tile
        {
            createObject(target.transform, objectToInstanciate.transform, new Vector3(0.0f, 0.0f, 0.0f));
            Destroy(target);
        }
        else if (target.GetComponent<HexagonalTile>() != null && target.GetComponent<HexagonalTile>().neighbours.up != null)
        {
            //if there is something on top of the tile than do nothing
        }
        else //if tile is not invisible place new tile on top of old tile
        {
            createObject(target.transform, objectToInstanciate.transform, new Vector3(0.0f, 1.0f, 0.0f));
        }
    }
}
