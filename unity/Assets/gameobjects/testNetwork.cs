using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testNetwork : MonoBehaviour {

    [SerializeField] GameObject conveyorBelt;
    [SerializeField] GameObject item;
    [SerializeField] private int numBelts;
    [SerializeField] private int numItems;
    [SerializeField] private Network network;
    private Vector3 position;

	// Use this for initialization
	void Start () {

        position = this.transform.position-=(new Vector3(0,0,0));

        GameObject prev = (GameObject)Instantiate(conveyorBelt, position, Quaternion.AngleAxis(-90, Vector3.forward));
        prev.transform.Rotate(new Vector3(0, -90, 0));
        position += new Vector3(1.725f, 0, 0);

        network.addInput(prev.transform);

        for (int i = 0; i < numItems; i++)
        {
            GameObject instance = (GameObject)Instantiate(item, position, Quaternion.AngleAxis(-90, Vector3.forward));
            prev.GetComponent<Node>().addTestItem(instance.transform);
        }

        for (int i = 0; i < numBelts; i++)
        {
            GameObject instance = (GameObject) Instantiate(conveyorBelt,position,Quaternion.AngleAxis(-90,Vector3.forward) );
            instance.transform.Rotate(new Vector3(0,-90,0));
            position += new Vector3(1.72f,0,0);

            prev.gameObject.GetComponent<Node>().addOutput(instance.GetComponent<Node>());
            prev = instance;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
