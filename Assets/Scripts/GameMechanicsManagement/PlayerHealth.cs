using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float currentPlayerHealth;
    [SerializeField] float maxPlayerHealth;

    // Start is called before the first frame update
    void Start()
    {
        currentPlayerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentPlayerHealth < 0)
        {
            Debug.Log(this.name + " is dead");
        }
    }

    public void DmgUnit(float dmgAmount)
    {
        currentPlayerHealth -= dmgAmount;
    }

    public void HealUnit (float healAmount)
    {
        if (currentPlayerHealth < maxPlayerHealth)
        {
            currentPlayerHealth += healAmount;
        }
        if (currentPlayerHealth > maxPlayerHealth)
        {
            currentPlayerHealth = maxPlayerHealth;
        }
    }
}
