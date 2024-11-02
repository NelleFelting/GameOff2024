using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class TriggerEvent : MonoBehaviour
{
    public UnityEvent UnityTriggerEvent;
    private bool _triggered;
    private void OnTriggerEnter(Collider other)
    {
        if (!_triggered && other.gameObject.CompareTag("Player"))
        {
            //player entered trigger zone
            TriggerEventHandler();
            _triggered = true;
        }
    }

    private void TriggerEventHandler()
    {
        UnityTriggerEvent?.Invoke();
    }
}
