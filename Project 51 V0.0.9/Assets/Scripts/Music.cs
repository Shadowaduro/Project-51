using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioClip calmMusic;
    public int timeTillCalm;
    [Space(5)]
    public AudioClip buildUpMusic;
    public int killsTillBuild;
    public int timeTillBuildUp;
    [Space(5)]
    public AudioClip epicMusic;
    public int killsTillEpic;
    [Space(5)]
    public AudioClip criticalMusic;
    public AudioClip bass;


    [HideInInspector]
    public AudioSource speaker;
    PlayerUI playerUI;

    public bool epicKillMode;
    public bool buildUpKillMode;
    public bool audioRegening;
    public bool audioIsActive;
    public int totalKills;
    public float currentTime;
    public bool fuckItMadeThisShitWork;

    void Start()
    {
        playerUI = GameObject.Find("UI").GetComponent<PlayerUI>();
        speaker = GetComponent<AudioSource>();
        PlayAudio(calmMusic);
    }

    public void GotKill()
    {
        if (speaker.volume < 0.99f && totalKills > 0 && fuckItMadeThisShitWork == true)
        {
            speaker.volume = 1;
            fuckItMadeThisShitWork = false;
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayAudio(criticalMusic);
        }
        
        MusicUpdate();

        if (buildUpKillMode == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeTillCalm)
            {
                if (speaker.volume == 1)
                {
                    speaker.PlayOneShot(bass, 0.5f);
                    fuckItMadeThisShitWork = true;
                }
                speaker.volume = Mathf.Lerp(speaker.volume, 0, 0.02f);

                if (speaker.volume <= 0.3f || audioRegening == true)
                {
                    if (audioRegening == false)
                    {
                        PlayAudio(calmMusic);
                        audioRegening = true;
                    }
                    speaker.volume = Mathf.Lerp(speaker.volume, 2, 0.02f);

                    if (speaker.volume >= 0.9f)
                    {
                        speaker.volume = 1;
                        currentTime = 0;
                        totalKills = 0;
                        audioRegening = false;
                        buildUpKillMode = false;
                        audioIsActive = false;
                    }
                }
            }
        }

        if (epicKillMode == true)
        {
            currentTime += Time.deltaTime;

            if (currentTime >= timeTillBuildUp)
            {
                if (speaker.volume == 1)
                {
                    speaker.PlayOneShot(bass, 0.5f);
                    fuckItMadeThisShitWork = true;
                }
                speaker.volume = Mathf.Lerp(speaker.volume, 0, 0.02f);

                if (speaker.volume <= 0.3f || audioRegening == true)
                {
                    if (audioRegening == false)
                    {
                        PlayAudio(buildUpMusic);
                        audioRegening = true;
                    }
                    speaker.volume = Mathf.Lerp(speaker.volume, 2, 0.02f);

                    if (speaker.volume >= 0.9f)
                    {
                        speaker.volume = 1;
                        currentTime = 0;
                        totalKills = killsTillBuild;
                        epicKillMode = false;
                        audioRegening = false;
                        buildUpKillMode = true;
                    }
                }
            }
        }
    }

    public void MusicUpdate()
    {

        if (totalKills >= killsTillBuild && epicKillMode == false && playerUI.pause == false && audioIsActive == false && buildUpKillMode == false)
        {
            if (speaker.volume == 1)
            {
                speaker.PlayOneShot(bass, 0.5f);
            }

            speaker.volume = Mathf.Lerp(speaker.volume, 0, 0.02f);

            if (speaker.volume <= 0.3f || audioRegening == true)
            {
                if (audioRegening == false)
                {
                    PlayAudio(buildUpMusic);
                    audioRegening = true;
                }

                speaker.volume = Mathf.Lerp(speaker.volume, 2, 0.02f);

                if (speaker.volume >= 0.9f)
                {
                    currentTime = 0;
                    speaker.volume = 1;
                    audioRegening = false;
                    buildUpKillMode = true;
                    audioIsActive = true;
                }
            }
        }

        if (totalKills >= killsTillEpic && playerUI.pause == false && buildUpKillMode == true)
        {
            if (speaker.volume == 1)
            {
                speaker.PlayOneShot(bass, 0.5f);
            }

            speaker.volume = Mathf.Lerp(speaker.volume, 0, 0.02f);

            if (speaker.volume <= 0.3f || audioRegening == true)
            {
                if (audioRegening == false)
                {
                    PlayAudio(epicMusic);
                    audioRegening = true;
                }

                speaker.volume = Mathf.Lerp(speaker.volume, 2, 0.02f);

                if (speaker.volume >= 0.9f)
                {
                    currentTime = 0;
                    speaker.volume = 1;
                    audioRegening = false;
                    epicKillMode = true;
                    buildUpKillMode = false;
                    audioIsActive = false;
                }
            }
        }
    }

    public void PlayAudio(AudioClip clip)
    {
        speaker.clip = clip;
        if (playerUI.pause == false)
        {
            speaker.Play();
        }
    }


}
