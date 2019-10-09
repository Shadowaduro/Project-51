using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAndInteraction : MonoBehaviour
{
    Vector2 mLook;
    Vector2 smoothVert;

    //public Shop shop;

    public float sensitivity = 1;
    public float smoothing = 3;
    public float intDistance;
    public float rayCastOffsetY;
    public float camXOffSet;
    public float camZOffSet;
    public Vector2 YClamp;
    //public Vector2 XClamp;

    GameObject charObj;
    public Transform target;


    void Start()
    {
        charObj = this.transform.parent.gameObject;
    }

    void Update()
    {
        //if (Input.GetButtonDown("Interact"))
        //{
        //    //shop.GetComponent<Shop>().Interaction();
        //}

        Vector3 newPos = new Vector3(target.position.x + camXOffSet, transform.position.y, target.position.z + camZOffSet);
        transform.position = newPos;


        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothVert.x = Mathf.Lerp(smoothVert.x, md.x, 1f / smoothing);
        smoothVert.y = Mathf.Lerp(smoothVert.y, md.y, 1f / smoothing);
        mLook += smoothVert;
        mLook.y = Mathf.Clamp(mLook.y, YClamp.x, YClamp.y);
        //SmLook.x = Mathf.Clamp(mLook.x, XClamp.x, XClamp.y);

        transform.localRotation = Quaternion.AngleAxis(-mLook.y, Vector3.right);
        charObj.transform.localRotation = Quaternion.AngleAxis(mLook.x, charObj.transform.up);
    }


}
