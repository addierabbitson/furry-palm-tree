using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Frisbee : MonoBehaviour
{
    public Transform m_goal;
    public Vector3 m_initialVelocity;
    public float m_curveStrength;

    Rigidbody m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!m_rigidbody.isKinematic)
        {
            Vector3 deviation = m_goal.position - transform.position;

            m_rigidbody.AddForce(deviation.normalized * m_curveStrength);
        }
    }
}
