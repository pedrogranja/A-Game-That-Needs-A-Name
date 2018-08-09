using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentTile : MonoBehaviour {

    private void Start()
    {
        name += "@" + transform.position.ToString();
    }

    public void OnTouchDown()
    {
        Debug.Log("I was clicked!\n" + name);
        GetComponentInChildren<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    }

}
