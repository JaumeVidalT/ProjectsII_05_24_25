using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public AudioSource openAudioSource;
    private Animation AnimatorDoor;
    public void PlayOpenAudio()
    {
        openAudioSource.Play();
    }
}
