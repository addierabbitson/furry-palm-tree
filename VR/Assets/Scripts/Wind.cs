using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public Vector3 m_windStrength;

    Frisbee m_frisbee;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_frisbee = other.GetComponent<Frisbee>();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_frisbee.Rigidbody.AddForce(m_windStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_frisbee = null;
        }
    }
}
