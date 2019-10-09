using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EwansEasterEgg : MonoBehaviour
{
    public float radius;
    public Color color;
    Animator animator;
    public Material lava;
    public Texture lavaTexture;
    public Texture lavaNormalMap;
    public Texture beansTexture;
    public Texture beansNormalMap;
    public Collider entrance;
    public float delay;
    public Text interactionText;
    public GameObject teleporter;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, radius);
        GetComponent<SphereCollider>().radius = radius;
    }

    void OnTriggerStay(Collider other)
    {
        interactionText.enabled = true;

        if (Input.GetButtonDown("Interact") && entrance.enabled == false)
        {
            animator.SetTrigger("Clicked");
            entrance.enabled = true;
            interactionText.enabled = false;
            StartCoroutine(EasterEggStart());
        }
    }

    void OnTriggerExit(Collider other)
    {
        interactionText.enabled = false;
    }

    IEnumerator EasterEggStart()
    {
        yield return new WaitForSeconds(delay);
        teleporter.SetActive(true);
        lava.SetTexture("_MainTex", beansTexture);
        lava.SetTexture("_BumpMap", null);
    }

    private void OnApplicationQuit()
    {
        lava.SetTexture("_MainTex", lavaTexture);
        lava.SetTexture("_BumpMap", lavaNormalMap);
    }
}
