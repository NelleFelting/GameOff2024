using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasController : MonoBehaviour
{
    public GameObject replacePipe;

    public bool isGasOff;
    public bool isLeakFixed;
    private ParticleSystem gas;

    //A bool to ensure the particle effect gets played only once
    private bool hasTriggered = false;

    [SerializeField] AudioSource gasLeak;
    private float randomPitch;

    // Start is called before the first frame update
    void Start()
    {
        //removes the objects parent
        this.transform.parent = null;

        //randomizes the steam sound pitch
        randomPitch = Random.Range(0.5f,1.5f);
        gasLeak.pitch = randomPitch;

        //gets the objects particle system
        gas = this.gameObject.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {   
        //if the leak has been fixed turn the gas off no matter what if the gas is on or off
        if (isLeakFixed == true)
        {
            gasLeak.Pause();
            gas.Stop();
        }
        else
        {
            //checks if the gas has been turned off and stops the gas
            if (isGasOff == true)
            {
                gas.Stop();
                gasLeak.Pause();
                hasTriggered = false;
            }
            else
            {
                //if the gas has not been turned off play the particle effect once
                if (hasTriggered == false)
                {
                    gas.Play();
                    if(!gasLeak.isPlaying)
                    {
                        gasLeak.Play();
                    }
                    hasTriggered = true;
                }
            }
        }        
    }
}
