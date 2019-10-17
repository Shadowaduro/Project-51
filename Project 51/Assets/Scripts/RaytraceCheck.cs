using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaytraceCheck : MonoBehaviour
{
    public GameObject raySpawn;
    public float rayDis;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 gunOrigin = new Vector3(raySpawn.transform.position.x, raySpawn.transform.position.y, raySpawn.transform.position.z);
        Debug.DrawRay(gunOrigin, raySpawn.transform.forward * rayDis, Color.magenta);
        RaycastHit hit;

        if (Physics.Raycast(gunOrigin, raySpawn.transform.forward, out hit, rayDis, ~LayerMask.GetMask("ImpactEffect")))
        {

            Debug.Log(hit.transform.name);

            if (hit.collider.tag == "Enemy")
            {
                //hit.collider.GetComponent<EnemyHealth>().HealthIsVisable(true);

            }
            if (hit.collider.tag == "")
            {
                //fix
                //Need to turn off when player looks away from emeny
                //hit.collider.GetComponent<EnemyHealth>().HealthIsVisable(false);

            }
        }
    }
}
