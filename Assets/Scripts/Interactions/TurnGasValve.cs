using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnGasValve : MonoBehaviour
{
    [SerializeField] Animator objectToAnimate;
    [SerializeField] string openBooleanName;
    [SerializeField] GameObject[] gasLeaksRight;
    [SerializeField] GameObject[] gasLeaksLeft;
    private bool isValveTurned = false;
    [SerializeField] AudioSource valveTurn;

    // Start is called before the first frame update
    void Start()
    {
        //Turns all gas leaks on the "left" OFF at the beginning of the game 
        if (gasLeaksLeft.Length > 0 )
        {
            for (int i = 0; i < gasLeaksLeft.Length; i++)
            {
                gasLeaksLeft[i].GetComponent<GasController>().isGasOff = true;
            }
        }      
    }

    public void OnInteraction()
    {
        isValveTurned = !isValveTurned;
        objectToAnimate.SetBool(openBooleanName, isValveTurned);
        if(!valveTurn.isPlaying)
        {
            valveTurn.Play();
        }

        for (int i = 0; i < gasLeaksRight.Length; i++)
            {
                //checks if the valve controlls any gas leaks then, then swtiches the gas leaks on or off depending on the side and the valve
                if (gasLeaksRight.Length > 0)
                {
                    gasLeaksRight[i].GetComponent<GasController>().isGasOff = isValveTurned;
                }
                if (gasLeaksLeft.Length > 0)
                {
                    gasLeaksLeft[i].GetComponent<GasController>().isGasOff = !isValveTurned;
                }
            }
    }
}
