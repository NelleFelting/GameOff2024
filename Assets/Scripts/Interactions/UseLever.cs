using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UseLever : MonoBehaviour
{
    public AudioSource audioSource;
    public Animator animator;
    public AudioClip powerOnClip;
    public AudioClip powerOffClip;
    public AudioClip leverLockedClip;
    public float leverAnimationTime = 0.5f;
    
    public UnityEvent LeverEvent;
    public UnityEvent CableEvent;

    public bool usingCable;
    public bool invertCableDirection;
    public float powerTime;
    public GameObject powerCable;

    private Material cableMaterial;


    private bool canBeTriggered;
    private bool leverLocked;
    public bool isDown;

    private void Start()
    {
        canBeTriggered = true;
        animator.SetBool("isDown", isDown);

        if (powerCable != null)
        {
            // Create a duplicate of the material at runtime
            Renderer cableRenderer = powerCable.GetComponent<Renderer>();
            cableMaterial = new Material(cableRenderer.material);
            cableRenderer.material = cableMaterial;
            cableMaterial.SetInt("_invertCableDirection", invertCableDirection ? 1 : 0);
        }
    }

    public void LockLever()
    {
        leverLocked = true;
    }

    public void TriggerLever()
    {
        if(!leverLocked && canBeTriggered)
        {
            canBeTriggered = false;
            if (usingCable)
            {
                if (isDown)
                {
                    audioSource.clip = powerOffClip;
                }
                else
                {
                    audioSource.clip = powerOnClip;
                }
            }
            audioSource.pitch = HelperScript.Deviate(1, .1f);
            audioSource.Play();
            animator.SetBool("isDown", !isDown);
            isDown = !isDown;
            StartCoroutine(WaitForLever());
        }

        else
        {
            audioSource.PlayOneShot(leverLockedClip);
        }
    }

    private void LeverEventHandler()
    {
        LeverEvent?.Invoke();
    }

    private void CableEventHandler()
    {
        CableEvent?.Invoke();
    }

    private IEnumerator WaitForLever()
    {
        yield return new WaitForSeconds(leverAnimationTime);
        
        LeverEventHandler();
        
        if (usingCable)
        {
            if (isDown)
            {
                StartCoroutine(HandleCablePower(1f));
            } else {
                StartCoroutine(HandleCablePower(0f)); 
            }
        } else {
            if (!leverLocked)
                canBeTriggered = true;
        }
    }

    private IEnumerator HandleCablePower(float targetPower)
    {
        bool isDownCable = isDown; // save lever state at time of cable power on
        float startPower = cableMaterial.GetFloat("_CablePower");
        float elapsedTime = 0f;

        while (elapsedTime < powerTime)
        {
            float cablePower = Mathf.Lerp(startPower, targetPower, elapsedTime / powerTime);
            cableMaterial.SetFloat("_CablePower", cablePower);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        cableMaterial.SetFloat("_CablePower", targetPower);
        if (!leverLocked)
            canBeTriggered = true;

        if (isDownCable)
        {
            CableEventHandler();
        }
    }

    private void OnDisable()
    {
        if (cableMaterial != null)
        cableMaterial.SetFloat("_CablePower", 0f);
    }
}   
