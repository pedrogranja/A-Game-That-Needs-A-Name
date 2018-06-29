using System.Collections.Generic;
using UnityEngine;
using ItemTransportation;

namespace Services
{
    /*
     * @author Pedro Granja 
     * Service used to interface with the ItemTransportation Module
     */
    public class ItemTransportationService
    {
        /**
         * Register the object toNotify to be called when the gameObject is ready to receive an item (method called is readyToAddItem()).
         * To use this method implement the AddRemoveItemInterface on the object that wants to call this method.
         * The GameObject must be part of the item transportation network item so they must have a Node and NodeTemplate script.
         */
        public static void callWhenReadyToAddItem(GameObject gameObject, AddRemoveItemInterface toNotify)
        {
            if (gameObject.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (gameObject.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            gameObject.GetComponents<Node>()[0].addToNotifyOnAcceptReady(toNotify);
        }


        /**
         * Register the object toNotify to be called when the gameObject is ready to output an item (method called is readyToRemoveItem()).
         * To use this method implement the AddRemoveItemInterface on the object that wants to call this method.
         * The GameObject must be part of the item transportation network item so they must have a Node and NodeTemplate script.
         */
        public static void callWhenReadyToRemoveItem(GameObject gameObject, AddRemoveItemInterface toNotify)
        {
            if (gameObject.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (gameObject.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            gameObject.GetComponents<Node>()[gameObject.GetComponents<Node>().Length-1].addToNotifyOnOutputReady(toNotify);
        }


        /**
         * Returns a view of a gameobject belonging to the item transportation network or null if its not part of network
         * (List of NodeVies because a gameobject may be represented by multiple nodes in the item transportation service, for example see furnace)
         */
        public static List<NodeView> getNodeView(GameObject gameObject)
        {
            return null;
        }

        /**
         * Returns a view of the network to which this gameobject belongs or null if its part of no network
         */
        public static NodeView getNetworkView(GameObject gameObject)
        {
            return null;
        }


        /**
         * Adds the gameObject to the network (if needed, if the gameObject belongs to another network than it joins both and
         * if it already belongs to the network than it simply connects the gameObject as an input to the targetNetworkNode) as an input to the 
         * targetNetworkNode. Both params must be part of the item transportation network so they must have a Node and NodeTemplate script.
         */
        public static void addInputToNode(GameObject gameObject, GameObject targetNetworkNode)
        {
            if (gameObject.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (gameObject.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            if (targetNetworkNode.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (targetNetworkNode.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            NetworkManager.getInstance().addInputToNode(gameObject,targetNetworkNode);
        }


        /**
        * Adds the gameObject to the network (if needed, if the gameObject belongs to another network than it joins both and
        * if it already belongs to the network than it simply connects the gameObject as an output to the targetNetworkNode) as an output to the 
        * targetNetworkNode. Both params must be part of the item transportation network so they must have a Node and NodeTemplate script.
        */
        public static void addOutputToNode(GameObject gameObject, GameObject targetNetworkNode)
        {
            if (gameObject.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (gameObject.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            if (targetNetworkNode.GetComponent<Node>() == null)
                throw new ItemTransportationException("Missing Node component in gameObject" + gameObject.GetInstanceID());

            if (targetNetworkNode.GetComponent<NodeTemplate>() == null)
                throw new ItemTransportationException("Missing NodeTemplate component in gameObject" + gameObject.GetInstanceID());

            NetworkManager.getInstance().addOutputToNode(gameObject, targetNetworkNode);
        }

    }
}
