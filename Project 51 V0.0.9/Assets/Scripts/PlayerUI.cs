using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{

    //public PlayerHealth pHealth;
    //public Text healthTextObj;
    //public Image healthBarImg;
    PlayerController player;
    public GameObject window;
    public GameObject mainUI;

    public Gun playerAmmo;
    Music music;
    public Text ammoTextObj;

    bool cursorLocked;
    [HideInInspector]
    public bool pause = true;

    public Transform oreSpawnLoc;
    public Transform battleSpawnLoc;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        music = GameObject.Find("Music").GetComponent<Music>();
        playerAmmo = GameObject.FindGameObjectWithTag("Player").GetComponent<Gun>();
        ToggleWindow(window);
        Invoke("UpdateUI", 0.2f);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleWindow(window);
        }
    }
    
    public void UpdateUI()
    {
        ammoTextObj.text = playerAmmo.currentAmmo + " / " + playerAmmo.maxAmmo;
        //healthTextObj.text = "Health: " + pHealth.currentHealth;
        //healthBarImg.fillAmount = pHealth.currentHealth / pHealth.maxHealth;
    }

    public void ToggleWindow(GameObject Window)
    {
        Window.SetActive(!Window.activeSelf);
        CursorChange();
        GamePause();
    }

    void CursorChange()
    {
        if (cursorLocked == false)
        {
            Cursor.visible = false; //Makes it invisble   
            Cursor.lockState = CursorLockMode.Locked; //Locks the cursor to centre of the scrren

            cursorLocked = true;
        }
        else if (cursorLocked == true)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            cursorLocked = false;
        }
    }

    public void GamePause()
    {
        pause = !pause; //Turns "pause" to the oppeset of that it's already

        if (pause)
        {
            Time.timeScale = 0;
            player.GetComponentInChildren<MouseLookAndInteraction>().enabled = false;
            player.GetComponentInChildren<PlayerController>().enabled = false;
            //mainUI.SetActive(false);
            music.speaker.Pause();
        }
        if (!pause)
        {
            Time.timeScale = 1;
            player.GetComponentInChildren<MouseLookAndInteraction>().enabled = true;
            player.GetComponentInChildren<PlayerController>().enabled = true;
            //mainUI.SetActive(true);
            music.speaker.Play();
        }
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void TeleportOre()
    {
        player.transform.position = oreSpawnLoc.position;
    }

    public void TeleportBattle()
    {
        player.transform.position = battleSpawnLoc.position;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

}
