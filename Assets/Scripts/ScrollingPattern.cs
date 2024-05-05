using System.Collections;
using System.Collections.Generic;
using System.Net.Security;
using UnityEngine;

public class ScrollingPattern : MonoBehaviour
{
    public float scrollSpeedX = 0.5f;
    public float scrollSpeedY = 0.5f;
    private MeshRenderer MeshRenderer;
    GameObject floor;

     void Start()
    {
        MeshRenderer = GetComponent<MeshRenderer>(); 
    }

     void Update()
    {
        MeshRenderer.material.mainTextureOffset = new Vector2(Time.realtimeSinceStartup * scrollSpeedX, Time.realtimeSinceStartup * scrollSpeedY);
    }
}
