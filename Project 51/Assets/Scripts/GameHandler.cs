using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject ketch;
    public GameObject mech;
    public Transform spawnLoc;
    public string character;
    // Start is called before the first frame update
    void Awake()
    {
        character = PlayerPrefs.GetString("selectedCharacter");

        if (character == "Ketch")
        {
            ketch.SetActive(true);
            ketch.transform.position = spawnLoc.position;
           //GameObject C = Instantiate(ketch, spawnLoc.position, Quaternion.identity);
           //C.name.Replace("(clone)", ketch.name);
        }
        else if (character == "Mech")
        {
            mech.SetActive(true);
            mech.transform.position = spawnLoc.position;
        }
    }
}
