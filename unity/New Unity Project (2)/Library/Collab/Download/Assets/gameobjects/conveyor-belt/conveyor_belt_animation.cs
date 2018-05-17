using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conveyor_belt_animation : MonoBehaviour {

    private Renderer renderer;
    [SerializeField] private float speed = 0.1f;

    // Use this for initialization
    void Start () {
        renderer = gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        renderer.materials[2].SetTextureOffset("_MainTex", new Vector2(speed,0) * Time.time);
	}
}
