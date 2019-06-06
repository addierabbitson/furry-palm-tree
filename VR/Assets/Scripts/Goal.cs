﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public FrisbeeRelease m_frisbeeRelease;
    public GameObject m_firework;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_firework.SetActive(true);

            Frisbee frisbee = other.GetComponent<Frisbee>();
            if (frisbee)
            {
                frisbee.OnGoal();
                m_frisbeeRelease.PlaceInHand();
            }
        }
    }
}
