using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public GameObject[] m_fireworks;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            foreach(GameObject firework in m_fireworks)
                firework.SetActive(true);

            Frisbee frisbee = other.GetComponent<Frisbee>();
            if (frisbee)
            {
                frisbee.OnGoal();
                frisbee.m_frisbeeRelease.PlaceInHand();
            }
        }
    }
}
