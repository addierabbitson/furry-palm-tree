using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Frisbee : MonoBehaviour
{
    public TrailRenderer m_trail;
    public Vector3 m_initialVelocity;
    public FrisbeeRelease m_frisbeeRelease;
    public float m_curveStrength;
    public bool m_useTimer;
    public float m_curveLength;
    public int m_score;
    public int m_combo;
    public int m_misses;

    Rigidbody m_rigidbody;
    float m_curveTimer;
    float m_deviation;
    [SerializeField]
    bool m_active;    

    public Rigidbody Rigidbody
    {
        get { return m_rigidbody; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_active)
        {
            if (m_useTimer)
            {
                m_curveTimer += Time.deltaTime;

                if (m_curveTimer >= m_curveLength)
                {
                    m_active = false;
                }
            }

            if (m_initialVelocity.x < 0 && m_rigidbody.velocity.x >= -m_initialVelocity.x)
            {
                m_active = false;
            }
            else if (m_initialVelocity.x >= 0 && m_rigidbody.velocity.x <= -m_initialVelocity.x)
            {
                m_active = false;
            }

            if (!m_rigidbody.isKinematic)
            {
                //float deviation = m_goal.position.x - transform.position.x;
                m_rigidbody.AddForce(new Vector3(m_deviation, 0, 0).normalized * m_curveStrength);
                //m_rigidbody.AddForce(new Vector3(m_curveStrength, 0, 0));
            }
        }
    }

    public void OnThrow()
    {
        m_deviation = -m_initialVelocity.x;
        m_curveStrength = transform.localRotation.eulerAngles.z;

        if (m_deviation < 0)
            m_curveStrength = m_curveStrength > 180 ? m_curveStrength - 360 : m_curveStrength;
        else
            m_curveStrength = m_curveStrength > 180 ? -(m_curveStrength - 360) : -m_curveStrength;

        m_curveStrength = 3 + (m_curveStrength / 180 * 5);
        m_active = true;
        m_curveTimer = 0;
    }

    public void OnGoal()
    {
        m_score++;
        m_combo++;
    }

    public void OnReset()
    {
        m_combo = 0;
        m_misses++;
    }
}
