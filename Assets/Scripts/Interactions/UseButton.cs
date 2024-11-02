using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UseButton : MonoBehaviour
{
    public AudioSource audioSource;
    public void TriggerButton()
    {
        audioSource.PlayOneShot(audioSource.clip);
    }
}
