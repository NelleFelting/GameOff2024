using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickUpController : MonoBehaviour
{
    private string pickUpMessage = "Pick Up (E)";
    private string dropMessage = "Drop (E)";
    PlayerInteraction playerInteraction;
    Interactable interactable;

    private float maxDistance = 4;

    [Header("pickup Settings")]
    private Transform holdPosition;
    private Rigidbody heldObjRB;
    private bool currentlyHolding = false;

    [Header("Physics Parameters")]
    [SerializeField] private float pickUpForce = 150.0f;

    void Start()
    {
        playerInteraction = GameObject.Find("FirstPersonController").GetComponent<PlayerInteraction>();
        interactable = this.GetComponent<Interactable>();
        heldObjRB = this.GetComponent<Rigidbody>();
        heldObjRB.useGravity = false;
        heldObjRB.isKinematic = true;
        holdPosition = GameObject.Find("HoldPosition").GetComponent<Transform>();
    }

    public void OnInteraction()
    {
        if(currentlyHolding == false)
        {
            PickUpObject();
        }
        else
        {
            DropObject();
        }
    }

    private void Update()
    {
        float objectPositionDistance = Vector3.Distance (holdPosition.transform.position, gameObject.transform.position);
        if (currentlyHolding == true)
        {
           MoveObject();
        }
        if (currentlyHolding == true && ((Input.GetKeyDown(KeyCode.E) && (playerInteraction.hit == false || playerInteraction.currentObject != this.gameObject)) || objectPositionDistance >= maxDistance))
        {
            DropObject();
        }
    }

    void MoveObject()
    {
        if (Vector3.Distance(gameObject.transform.position, holdPosition.position) > 0.1f)
        {
            Vector3 moveDirection = (holdPosition.position - gameObject.transform.position);
            heldObjRB.AddForce(moveDirection * pickUpForce);
        }
    }

    void PickUpObject()
    {
        interactable.displayMessage = dropMessage;        
        heldObjRB.useGravity = false;
        heldObjRB.isKinematic = false;
        heldObjRB.drag = 10;
        heldObjRB.constraints = RigidbodyConstraints.FreezeRotation;

        heldObjRB.transform.parent = holdPosition;
        currentlyHolding = true;
    }

    public void DropObject()
    {
        interactable.displayMessage = pickUpMessage;
        heldObjRB.useGravity = true;
        heldObjRB.drag = 1;
        heldObjRB.constraints = RigidbodyConstraints.None;
        
        gameObject.transform.parent = null;
        currentlyHolding = false;
    }
}
