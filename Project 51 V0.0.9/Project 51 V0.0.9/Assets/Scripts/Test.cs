using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
   
    public Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // The mesh goes red when the mouse is over it...
    void OnMouseEnter()
    {
        rend.material.color = Color.red;
    }

    // ...the red fades out to cyan as the mouse is held over...
    void OnMouseOver()
    {
        rend.material.color -= new Color(0.1F, 0, 0) * Time.deltaTime * 10;
    }

    // ...and the mesh finally turns white when the mouse moves away.
    void OnMouseExit()
    {
        rend.material.color = Color.white;
    }
}

