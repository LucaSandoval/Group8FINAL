using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour
{
    private float stepTime = 0.6f;
    private float timer;

    private SoundPlayer soundPlayer;

    public void Start()
    {
        soundPlayer = GameObject.FindGameObjectWithTag("SoundController").GetComponent<SoundPlayer>();
        timer = stepTime;
    }

    public void FixedUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            } else
            {
                soundPlayer.PlaySoundRandomPitch("Step", 0.2f);
                timer = stepTime;
            }
        }
    }
}
