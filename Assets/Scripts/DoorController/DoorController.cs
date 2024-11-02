using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip openClip;
    public AudioClip closeClip;
    public bool startOpen;

    public Animator animator;
    private bool isOpen;
    private bool isUsable;
    public float lockOutTime = 1f;
    private float audioSourceStartingPitch;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("isOpen", isOpen);
        audioSourceStartingPitch = audioSource.pitch;
        isUsable = true;
        if (startOpen)
        {
            TriggerDoor();
        }
    }

    public void TriggerDoor()
    {
        if (isUsable)
        {
            isUsable = false;
            if(startOpen)
            {
                audioSource.volume = 0f;
            }
            audioSource.pitch = HelperScript.Deviate(audioSourceStartingPitch, 0.2f);
            if (isOpen)
            {
                audioSource.PlayOneShot(closeClip);
            } else
            {
                audioSource.PlayOneShot(openClip);
            }

            isOpen = !isOpen;
            animator.SetBool("isOpen", isOpen);
            StartCoroutine(LockOutTimer(lockOutTime)); 
        }
    }

    private IEnumerator LockOutTimer(float lockOutTime)
    {
        yield return new WaitForSeconds(lockOutTime);
        startOpen = false;
        isUsable = true;
    }
}