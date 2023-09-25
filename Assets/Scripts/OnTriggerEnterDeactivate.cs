using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerEnterDeactivate : MonoBehaviour
{
    public UnityEvent coinCollected;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(Tags.PLAYER))
        {
            coinCollected.Invoke();
        }
    }
}
