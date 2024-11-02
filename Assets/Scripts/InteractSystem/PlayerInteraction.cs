using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float playerReach = 3f;
    Interactable currentInteractable;
    [HideInInspector] public bool hit = false;
    [HideInInspector] public GameObject currentObject;
    [SerializeField] LayerMask interactionLayerMask;

    // Update is called once per frame
    void Update()
    {
        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interact();
        }
    }

    void CheckInteraction()
    {
        //Raycasts from the camera 
        RaycastHit hit;
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        
        //if collides with anything in player reach
        if (Physics.Raycast(ray, out hit, playerReach)) 
        {
            if (((1 << hit.collider.gameObject.layer) & interactionLayerMask) != 0)//if looking at interactable object
            {
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (newInteractable == null) // if the object does not have an Interactable script attached
                {
                    DisableCurrentInteractable();
                    return;
                }
                currentObject = hit.transform.gameObject;

                if (newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                }
                else //if new interactable is not enabled
                {
                    DisableCurrentInteractable();
                }

            }
            else //if not interactable
            {
                DisableCurrentInteractable();
            }

        }
        else //if nothing is in reach
        {
            DisableCurrentInteractable();
        }
    }

    void SetNewCurrentInteractable(Interactable newInteractable)
    {
        hit = true;
        currentInteractable = newInteractable;
        HUDController.instance.EnableInteractionText(currentInteractable.displayMessage);
    }

    void DisableCurrentInteractable()
    {
        hit = false;
        HUDController.instance.DisableInteractionText();
        if (currentInteractable)
        {
            currentInteractable = null;
        }
    }
}
