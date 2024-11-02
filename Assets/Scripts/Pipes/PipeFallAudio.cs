using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeFallAudio : MonoBehaviour
{

    [SerializeField] AudioSource pipeFall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        pipeFall.Play();
        this.gameObject.SetActive(false);
    }
}
