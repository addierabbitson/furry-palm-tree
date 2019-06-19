using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Frisbee : MonoBehaviour
{
    public TrailRenderer[] m_trails;
    public ParticleSystem[] m_particles;
    public FrisbeeRelease m_frisbeeRelease;

    Rigidbody m_rigidbody;
    Vector3 m_initialVelocity;
    float m_deviation;
    int m_score;
    int m_combo;
    int m_misses;
    bool m_active;
    float m_curveStrength;

    #region Getter/Setter
    public Rigidbody Rigidbody
    {
        get { return m_rigidbody; }
    }
    public Vector3 InitialVelocity
    {
        get { return m_initialVelocity; }
        set { m_initialVelocity = value; }
    }
    public int Score
    {
        get { return m_score; }
    }
    public int Combo
    {
        get { return m_combo; }
    }
    public int Misses
    {
        get { return m_misses; }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (m_active)
        {
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

        foreach (TrailRenderer trail in m_trails)
        {
            trail.gameObject.SetActive(true);
        }
        foreach (ParticleSystem particle in m_particles)
        {
            particle.gameObject.SetActive(true);
        }
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

    public void OnInHand()
    {
        foreach (TrailRenderer trail in m_trails)
        {
            trail.gameObject.SetActive(false);
            trail.Clear();
        }
        foreach (ParticleSystem particle in m_particles)
        {
            particle.gameObject.SetActive(false);
            particle.Clear();
        }
    }
}
