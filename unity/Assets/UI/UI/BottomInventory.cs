using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomInventory : MonoBehaviour {

    [SerializeField] private GameObject prefabButton;
    [SerializeField] private int numberOfSlots = 10;
    [SerializeField] private int slotSize = 50;

    // Use this for initialization
    void Start()
    {
        int width = Screen.width;
        int height = Screen.height;

        prefabButton.GetComponent<RectTransform>().sizeDelta = new Vector2(slotSize, slotSize);

        int initialXPos = ( width - slotSize * (numberOfSlots-1) ) / 2;

        for (int i = 0; i < numberOfSlots; i++)
        {
            GameObject button = (GameObject)Instantiate(prefabButton);
            button.transform.SetParent(transform, false);
            button.transform.position = new Vector3(initialXPos + i * slotSize, slotSize / 2, 0);
        }
    }
}
