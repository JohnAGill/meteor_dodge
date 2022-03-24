using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetScrolling : MonoBehaviour
{
    // Start is called before the first frame update
   public GameObject quadGameObject;
    private Renderer quadRenderer;

    public float scrollSpeed;

void Start()
{
    quadRenderer = quadGameObject.GetComponent<Renderer>();
}

void Update()
{
    Vector2 textureOffset = new Vector2(0, Time.time*scrollSpeed);
    quadRenderer.material.mainTextureOffset = textureOffset;
}
}
