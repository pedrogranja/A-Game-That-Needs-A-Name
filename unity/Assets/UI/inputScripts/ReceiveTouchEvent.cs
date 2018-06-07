using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveTouchEvent : MonoBehaviour {

    enum PlayerState {EDITORMODE,SURVIVALMODE};
    private PlayerState currentState;

    void Start()
    {
        currentState = PlayerState.EDITORMODE;
    }

    void OnTouchDown()
    {
        switch(currentState)
        {
            case PlayerState.SURVIVALMODE:
                break;

            case PlayerState.EDITORMODE:
                ObjectCreationEditorMode objectCreation = ObjectCreationEditorMode.getInstance();
                objectCreation.OnTouchDown(gameObject);
                break;

            default:
                Debug.Log("Player state not recognized");
                break;
        }
    }

    void ObjectDeselected()
    {
    }

}
