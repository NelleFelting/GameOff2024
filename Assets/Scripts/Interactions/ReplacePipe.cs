using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class ReplacePipe : MonoBehaviour
{
    [Header("Repair Type")]
    [SerializeField] bool isSmallRepair = false;
    [SerializeField] bool isMediumRepair = false;
    [SerializeField] bool isLargeRepair = false;
    Interactable interactable;
    PipeController pipeType;

    public bool isPipeFixed = false;
    private GameObject repairPipe;

    private bool hasCollidedWith = false;
    private int collisions;

    [SerializeField] GameObject gasLeak;
    private float randomPitch;

    [SerializeField] AudioSource pipeInteraction;

    // Start is called before the first frame update
    void Start()
    {
        interactable = this.GetComponent<Interactable>();

        this.transform.parent = null;
        randomPitch = Random.Range(0.5f, 1.5f);
        pipeInteraction.pitch = randomPitch;
    }

    // Update is called once per frame
    void Update()
    {
        if (collisions == 0)
        {
            interactable.displayMessage = "Find replacement pipe";
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        collisions++;
        repairPipe = col.gameObject;
        if (repairPipe.GetComponent<PipeController>() == null)
        {
            interactable.displayMessage = "That is not a pipe";
            Debug.LogWarning($"{col.gameObject} is not a pipe");
            return;
        }
        pipeType = repairPipe.GetComponent<PipeController>();

        if (pipeType.isBroken == false)
        {
            if (gasLeak.GetComponent<GasController>().isGasOff == true)
            {
                if (isSmallRepair == true && pipeType.isSmall == true)
                {
                    RepairThePipe(repairPipe);
                }
                else
                {
                    interactable.displayMessage = "Pipe is the wrong Size!";
                }

                if (isMediumRepair == true && pipeType.isMedium == true)
                {
                    RepairThePipe(repairPipe);
                }
                else
                {
                    interactable.displayMessage = "Pipe is the wrong Size!";
                }

                if (isLargeRepair == true && pipeType.isLarge == true)
                {
                    RepairThePipe(repairPipe);
                }
                else
                {
                    interactable.displayMessage = "Pipe is the wrong Size!";
                }
            }
            else
            {
                interactable.displayMessage = "Turn the gas off!";
            }
        }
        else
        {
            if (hasCollidedWith == true)
            {
                interactable.displayMessage = "Find unbroken pipe to fix leak";
            }           
        }
    }

    private void OnTriggerExit(Collider col)
    {
        collisions--;
        if (col.GetComponent<PipeController>() != null && col.GetComponent<PipeController>().isBroken == true)
        {
            hasCollidedWith = true;
        }
    }

    void RepairThePipe(GameObject repairPipe)
    {
        repairPipe.GetComponent<Rigidbody>().isKinematic = true;
        repairPipe.GetComponent<PickUpController>().DropObject();
        repairPipe.transform.position = gameObject.transform.position;
        repairPipe.transform.rotation = gameObject.transform.rotation;
        pipeInteraction.Play();
        gasLeak.GetComponent<GasController>().isLeakFixed = true;

        repairPipe.GetComponent<Interactable>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
