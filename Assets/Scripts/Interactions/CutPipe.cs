using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutPipe : MonoBehaviour
{
    PickUpController pickUpController;
    Interactable interactable;
    [SerializeField] GasController gasLeak;
    [SerializeField] GameObject replacePipe;
    private bool isCut = false;
    private float randomPitch;
    [SerializeField] AudioSource pipeInteract;

    private void Start()
    {
        //gets the pickup controller for the object to allow the object to be carried
        pickUpController = this.gameObject.GetComponent<PickUpController>();

        //gets the objects interactable component to be able to change the text
        interactable = this.GetComponent<Interactable>();

        //randomizes the pitch of the pipe collision sound
        randomPitch = Random.Range(0.5f, 1.5f);
        pipeInteract.pitch = randomPitch;
    }

    private void Update()
    {
        //checks if the gas has been turned off and the pipe isn't cut then switches the interact message to cut
        if (gasLeak.GetComponent<GasController>().isGasOff == true && isCut == false)
        {
            interactable.displayMessage = "Cut (E)";
        }
    }

    public void Interact()
    {
        //checks if the pipe has been cut
        if (isCut == false)
        {
            //checks if the gas is turned off
            if (gasLeak.GetComponent<GasController>().isGasOff == true)
            {
                //plays a sound, makes the replace pipe collision box active, picks up the object and then sets the object to have been cut
                pipeInteract.Play();
                replacePipe.gameObject.SetActive(true);
                pickUpController.OnInteraction();
                isCut = true;
            }
            else
            {
                //changes the display message to tell the player to turn the gas off first
                interactable.displayMessage = "Turn gas off first!";
            }
        }
        else
        {
            //if the pipe has already been cut, just pick it up or drop it
            pickUpController.OnInteraction();
        }

    }
}
