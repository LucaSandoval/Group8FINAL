using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceController : MonoBehaviour
{
    public GameObject normalLighting;
    public GameObject scaryLighting;
    public GameObject rubble;
    public GameObject lockedDoor;
    public GameObject unlockedDoor;
    public GameObject nonHiddenStuff;
    public GameObject hiddenstuff;

    private SoundPlayer soundPlayer;

    public void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        SushiSequence();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            SushiSequence();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            ScarySequence();
        }
    }

    public void SushiSequence()
    {
        soundPlayer.PauseSound("Scary");
        soundPlayer.PauseSound("PowerOff");

        soundPlayer.PlaySound("SushiMusic");
        soundPlayer.PlaySound("Crowd");

        normalLighting.SetActive(true);
        scaryLighting.SetActive(false);
    }

    public void ScarySequence()
    {
        soundPlayer.PauseSound("SushiMusic");
        soundPlayer.PauseSound("Crowd");

        soundPlayer.PlaySound("Scary");
        soundPlayer.PlaySound("PowerOff");

        normalLighting.SetActive(false);
        scaryLighting.SetActive(true);
    }
}
