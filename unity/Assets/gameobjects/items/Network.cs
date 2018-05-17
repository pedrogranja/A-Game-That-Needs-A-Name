using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour {

    [SerializeField] private List<Transform> inputNodes;


    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        updateNodes();
	}


    //breath first but all outputs of a node need to be visited before the node itself is visited
    void updateNodes()
    {
        List<Node> toUpdate = new List<Node>();
        List<Node> auxiliar = new List<Node>();
        HashSet<Node> visited = new HashSet<Node>();

        foreach(Transform node in inputNodes)
        {
            toUpdate.Insert(0,node.GetComponents<Node>()[0]);
        }

        while (toUpdate.Count > 0)
        {
            auxiliar = new List<Node>();

            foreach (Node node in toUpdate)
            {
                if (visited.Contains(node))
                    continue;

                node.update();
                visited.Add(node);

                foreach (Node nodeToAtualize in node.getOutputs())
                {
                    auxiliar.Add(nodeToAtualize);
                }

            }
            toUpdate = auxiliar;
        }
    }


    public void addInput(Transform input)
    {
        if (inputNodes == null)
            inputNodes = new List<Transform>();

        inputNodes.Add(input);
    }

}
