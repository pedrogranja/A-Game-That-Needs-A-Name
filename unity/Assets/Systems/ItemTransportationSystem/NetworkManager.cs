using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ItemTransportation
{
    public class NetworkManager : MonoBehaviour
    {

        [SerializeField] private List<Transform> inputNodes;
        private static NetworkManager reference;

        private NetworkManager()
        {

        }

    
        // Use this for initialization
        void Start()
        {
            Application.targetFrameRate = 30;
        }


        // Update is called once per frame
        void FixedUpdate()
        {
            updateNodes();
        }


        //breath first but all outputs of a node need to be visited before the node itself is visited
        void updateNodes()
        {
            List<Node> toUpdate = new List<Node>();
            List<Node> auxiliar = new List<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            if (inputNodes == null)
                return;

            foreach (Transform node in inputNodes)
            {
                toUpdate.Insert(0, node.GetComponents<Node>()[0]);
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


        public void addInputToNode(GameObject gameObject, GameObject targetNetworkNode)
        {
            if(this.inputNodes.Contains(targetNetworkNode.transform))
            {
                removeInput(targetNetworkNode.transform);
                addInput(gameObject.transform);
            }

            gameObject.GetComponent<Node>().addOutput(targetNetworkNode.GetComponent<Node>());
        }


        public void addOutputToNode(GameObject gameObject, GameObject targetNetworkNode)
        {
            targetNetworkNode.GetComponents<Node>()[targetNetworkNode.GetComponents<Node>().Length-1].addOutput(gameObject.GetComponents<Node>()[0]);
        }


        public void addInput(Transform input)
        {
            if (inputNodes == null)
                inputNodes = new List<Transform>();

            inputNodes.Add(input);
        }


        public void removeInput(Transform inputToRemove)
        {
            inputNodes.Remove(inputToRemove);
        }


        public List<Transform> getInputNodes()
        {
            return this.inputNodes;
        }


        private List<Node> getNodesInNetwork()
        {

            List<Node> auxiliar2 = new List<Node>();
            List<Node> auxiliar = new List<Node>();
            List<Node> result = new List<Node>();
            HashSet<Node> visited = new HashSet<Node>();

            foreach (Transform node in this.inputNodes)
            {
                auxiliar.Insert(0, node.GetComponents<Node>()[0]);
            }

            while (auxiliar.Count > 0)
            {
                auxiliar2 = new List<Node>();

                foreach (Node node in auxiliar2)
                {
                    if (visited.Contains(node))
                        continue;

                    result.Add(node);
                    visited.Add(node);

                    foreach (Node nodeToAtualize in node.getOutputs())
                    {
                        auxiliar2.Add(nodeToAtualize);
                    }

                }
                auxiliar = auxiliar2;
            }

            return result;
        }


        public static NetworkManager getInstance()
        {
            if (reference == null)
            {
                GameObject gameObject = new GameObject();
                gameObject.name = "NetworkManager";
                reference = gameObject.AddComponent<NetworkManager>();
            }

            return reference;
        }
    }
}
