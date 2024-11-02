using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionAudio : MonoBehaviour
{ 
    private Rigidbody objRB;
    private float currentSpeed;
    [SerializeField] float minSoundSpeed = 0.6f;
    private GameObject player;
    private float randomPitch;


    [SerializeField] AudioSource pipeInteract;
    // Start is called before the first frame update
    void Start()
    {
        //gets the objects rigid body
        objRB = this.GetComponent<Rigidbody>();
        //finds the player object
        player = GameObject.FindGameObjectWithTag("Player");

        //randomizes the pitch for the collision sound
        randomPitch = Random.Range(0.5f, 1.5f);
        pipeInteract.pitch = randomPitch;
    }

    private void OnCollisionEnter(Collision other)
    {
        //checks of the object the pipe has collided with is the player, if it isn't play the interact sound
        if (other.gameObject != player)
        {
            currentSpeed = objRB.velocity.magnitude;
            if (currentSpeed > minSoundSpeed)
            {
                pipeInteract.Play();
            }
        }
    }
}
