using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {         
            Frisbee frisbee = other.GetComponent<Frisbee>();
            if (frisbee)
            {
                frisbee.OnReset();
                frisbee.m_frisbeeRelease.PlaceInHand();
            }
        }
    }
}
