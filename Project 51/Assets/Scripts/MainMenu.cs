using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject characterSelect;
    public GameObject windowBefore;
    // Start is called before the first frame update
    void Start()
    {
        mainMenu.SetActive(true);
        characterSelect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectCharacter()
    {
        windowBefore = GameObject.FindGameObjectWithTag("Window");
        windowBefore.SetActive(!windowBefore.activeSelf);
        characterSelect.SetActive(!characterSelect.activeSelf);
    }
    public void Back()
    {
        windowBefore.SetActive(!windowBefore.activeSelf);
        windowBefore = GameObject.FindGameObjectWithTag("Window");
        characterSelect.SetActive(!characterSelect.activeSelf);
    }
    public void SaveCharater(Button button)
    {

        if (button.name == "Acid Alien Button")
        {
            PlayerPrefs.SetString("selectedCharacter", "Ketch");
        }
        else if (button.name == "Mech Button")
        {
            PlayerPrefs.SetString("selectedCharacter", "Mech");
        }
        
        SceneManager.LoadScene("Test Scene");

    }
}
