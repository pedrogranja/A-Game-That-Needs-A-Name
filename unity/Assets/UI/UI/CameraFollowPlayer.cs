using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour {
    public Transform player;
    
    [SerializeField]
    private Vector3 relativePosition;

    [SerializeField]
    private float height;

	// Use this for initialization
	void Start () {
        relativePosition = transform.position - player.position;
        height = transform.position.y;
	}
	
	// Update is called once per frame
	void Update () {
        //transform.LookAt(player);
        Quaternion newRotation = Quaternion.LookRotation(player.position - transform.position);

        Vector3 newPosition = player.position + relativePosition;
        newPosition.y = height;

        transform.position = Vector3.Lerp(transform.position, newPosition, 0.1f);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, 0.1f);
    }
}
