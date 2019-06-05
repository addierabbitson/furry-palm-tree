using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public int m_score;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Frisbee")
        {
            m_score++;
        }
    }
}
