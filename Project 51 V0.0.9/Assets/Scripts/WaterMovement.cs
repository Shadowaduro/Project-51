
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMovement : MonoBehaviour                                                                                                               //Made by Jethro, Ewan Sucks
{
    public float flowSpeed = 1;
    public float secondaryFlowSpeed = 0.5f;
    public bool flow = true;
    public Material mat;
    private float timePassed;
    private void Start()
    {
        mat.SetTextureOffset("_MainTex", new Vector2(0, 0));
        mat.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, 0));
    }
    void Update()
    {
        if(flow)
        {
            timePassed += Time.deltaTime;
            mat.SetTextureOffset("_MainTex", new Vector2(0, -timePassed * flowSpeed));
            mat.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, -timePassed * secondaryFlowSpeed));
        }
    }
}























































































































































