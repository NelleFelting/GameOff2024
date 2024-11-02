using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

public class PipeDoorController : MonoBehaviour
{
    [SerializeField] Animator objectToAnimate;
    [SerializeField] string openBooleanName;
    [SerializeField] bool isLocked;

    [SerializeField] private AudioSource doorOpen;
    [SerializeField] private AudioSource lockJiggle;

    [SerializeField] List<GameObject> pipesThatNeedFixing;
    private List<bool> canPipeBeFixed;
    private List<bool> replacePipe;

    private float pipesRepaired = 0;
    private float numberOfBrokenPipes;

    Interactable interactable;

    void Start()
    {
        isLocked = true;
        interactable = this.GetComponent<Interactable>();
        numberOfBrokenPipes = pipesThatNeedFixing.Count;
        //set the the length of the canPipeBeFixed list to the same length as the number of pipes needing to be fixed.
        if (pipesThatNeedFixing.Count > 0)
        {
            for (int i = 0; i < pipesThatNeedFixing.Count; i++)
            {
                canPipeBeFixed.Add(pipesThatNeedFixing[i]);
                canPipeBeFixed[i] = false;
            }
        }
    }

    void Update()
    {
        //Checks if the replace pipe object is active in the scene, meaning the pipe has been cut and sets the canPipeBeFixed bool to true.
        if (pipesThatNeedFixing.Count > 0)
        {
            for (int i = 0; i < pipesThatNeedFixing.Count; i++)
            {
                if (pipesThatNeedFixing[i].activeSelf == true)
                {
                    canPipeBeFixed[i] = true;
                }
                //Checks if the canPipeBeFixed bool is true and if the replace pipe object is active in the scene, if it isn't, then this means the pipe has been fixed.
                if (canPipeBeFixed[i] == true && pipesThatNeedFixing[i].activeSelf == false)
                {
                    pipesRepaired++;
                    pipesThatNeedFixing.Remove(pipesThatNeedFixing[i]);
                    canPipeBeFixed.Remove(canPipeBeFixed[i]);
                }
            }
        }

        //Unlocks the door if all the pipes are fixed
        if (pipesRepaired == numberOfBrokenPipes)
        {
            isLocked = false;
            interactable.displayMessage = "Open (E)";
        }
    }
    public void OnInteraction()
    {
        //Plays the door opening animation when door is locked, and distance to OBJECT is less than interactive distance
        if (isLocked == false)
        {
            Debug.Log("Door opens");
            doorOpen.Play();
            objectToAnimate.SetBool(openBooleanName, true);
            this.GetComponent<Interactable>().enabled = false;
        }
        else
        {
            if(!lockJiggle.isPlaying)
            {
                lockJiggle.Play();
            }
            
            interactable.displayMessage = "The door is locked";
        }
    }
}

