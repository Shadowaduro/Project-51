using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnables : MonoBehaviour
{
    public List<GameObject> spawnObjs;
    public int maxSpawnAmount;
    public int minSpawnAmount;
    private int totalSpawned;

    public GameObject spawnObj;
    
    void Start()
    {
        //grabs all of the transforms of the children of the parents and increases the spawnObjs.Count size
        foreach (Transform r in transform)
        {
            spawnObjs.Add(r.gameObject);
        }

        
        //sets sN as a hole number to a random number between minSpawnAmount and maxSpawnAmount
        int sN = Random.Range(minSpawnAmount, maxSpawnAmount);
        Debug.Log(sN);

        for (int i = 0; i < sN; i++)
        {
            //gives p a random number out of spawnObjs.count
            int p = Random.Range(0, spawnObjs.Count);

            if (totalSpawned < sN)
            {
                //spawns on random transform from spawnObjs list in the scene
                Instantiate(spawnObj, spawnObjs[p].transform.position, spawnObj.transform.rotation);
                totalSpawned++;
                //deletes the spwan point so it cant spawn 2 times on the same spot
                spawnObjs.Remove(spawnObjs[p]);
            }
        }
    }
}
