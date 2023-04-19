using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "New Sound", menuName = "Sound Effects")]
public class Sound : ScriptableObject
{
    public string soundName; // The name of the sound to be called in any of the SoundController functions.

    public AudioClip clip; // The clip itself (.wav, .mp3, etc.) 

    [Range(0, 3f)]
    public float baseVolume = 1; // Base volume of the clip (can be lowered by fade in/out, but this value will not change.)
    [Range(0.2f, 3f)]
    public float basePitch = 1;

    public bool loop; // Should this sound loop itself? 

    public AudioMixerGroup audioMixerGroup;

    [HideInInspector]
    public AudioSource source; // AudioSource associated with this sound.
}