using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telporter : MonoBehaviour
{
    public GameObject player;
    public Transform toPos;
    public bool t;

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("1");
        if (other.tag == "Player")
        {
            player = other.gameObject;
            StartCoroutine(Teleporting());
        }
    }

    IEnumerator Teleporting()
    {
        yield return new WaitForSeconds(3);
        //Debug.Log("2");
        player.GetComponent<PlayerController>().enabled = false;
        //Debug.Log(player.transform.position);
        player.transform.position = toPos.position;
        t = true;
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        //Debug.Log("Canceled");
    }

    private void Update()
    {
        if (t == true)
        {
            if (player.transform.position == toPos.position)
            {
                player.GetComponent<PlayerController>().enabled = true;
                //Debug.Log(player.transform.position);
                player = null;
                t = false;
            }
        }
    }
}
