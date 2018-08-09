using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentBlocksEffect : MonoBehaviour {

    public RenderTexture transparentBlocksRenderTexture;
    public Material mat;

    // Use this for initialization
    void Start () {
        transparentBlocksRenderTexture.width = Screen.width;
        transparentBlocksRenderTexture.height = Screen.height;
    }

    // Update is called once per frame
    void Update () {

    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination);
        Graphics.Blit(transparentBlocksRenderTexture, destination, mat);
    }

}
