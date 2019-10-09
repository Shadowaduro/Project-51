using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour
{
    public GameObject followObj;

    void Start()
    {
        followObj = Camera.main.gameObject; 
    }

    // Update is called once per frame
    void Update()
    {
        if(followObj != null)
        {
            Vector3 targetPos = new Vector3(followObj.transform.position.x, transform.position.y + 1, followObj.transform.position.z);
            transform.LookAt(targetPos);
        }
        
    }
}
