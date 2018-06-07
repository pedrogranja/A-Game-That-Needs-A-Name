using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchScreenInput : MonoBehaviour {

    public LayerMask touchInputMask;
    [SerializeField] private Camera camera;
    private RaycastHit hit;
    private GameObject prevGameObject;

    // Update is called once per frame
    void Update () {

#if UNITY_EDITOR
        if (Input.GetMouseButton(0) || Input.GetMouseButtonDown(0) || Input.GetMouseButtonUp(0))
        {

            Ray ray = camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, touchInputMask))
            {
                GameObject recipient = hit.transform.gameObject;

                if(prevGameObject != null && prevGameObject != recipient)
                    prevGameObject.SendMessage("ObjectDeselected", hit.point, SendMessageOptions.DontRequireReceiver);

                if (Input.GetMouseButtonDown(0))
                {
                    recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                }

                prevGameObject = recipient;
            }
            
        }
#endif

        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                Ray ray = Camera.current.ScreenPointToRay(touch.position);

                if(Physics.Raycast(ray, out hit, touchInputMask))
                {
                    GameObject recipient = hit.transform.gameObject;

                    if (prevGameObject != null && prevGameObject != recipient)
                        prevGameObject.SendMessage("ObjectDeselected", hit.point, SendMessageOptions.DontRequireReceiver);

                    if (touch.phase == TouchPhase.Began)
                    {
                        recipient.SendMessage("OnTouchDown", hit.point, SendMessageOptions.DontRequireReceiver);
                    }

                    prevGameObject = recipient;
                }
            }
        }
		
	}
}
