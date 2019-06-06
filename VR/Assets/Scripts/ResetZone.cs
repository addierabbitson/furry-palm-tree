using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetZone : MonoBehaviour
{
    public FrisbeeRelease m_frisbeeRelease;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_frisbeeRelease.PlaceInHand();

            Frisbee frisbee = other.GetComponent<Frisbee>();
            if (frisbee)
                frisbee.OnReset();
        }
    }
}
