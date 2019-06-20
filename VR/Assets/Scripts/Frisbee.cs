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
        if (m_rigidbody.velocity.sqrMagnitude > 0) // faces the frisbee in the direction of movement
            transform.rotation = Quaternion.LookRotation(m_rigidbody.velocity.normalized, transform.up);

        // if the deviation force should be applied
        if (m_active)
        {
            // once the velocity of the rigidbody reaches the negative value of the initial velocity stop applying the deviation
            if (m_initialVelocity.x < 0 && m_rigidbody.velocity.x >= -m_initialVelocity.x)
            {
                m_active = false;
            }
            else if (m_initialVelocity.x >= 0 && m_rigidbody.velocity.x <= -m_initialVelocity.x)
            {
                m_active = false;
            }

            // if the rigidbody is not in hand
            if (!m_rigidbody.isKinematic)
            {                
                m_rigidbody.AddForce(new Vector3(m_deviation, 0, 0).normalized * m_curveStrength); // apply constant force in the opposite direction of the initial velocity            
            }
        }
    }

    /// <summary>
    /// Handles setting up the frisbee for flight
    /// </summary>
    public void OnThrow()
    {
        m_deviation = -m_initialVelocity.x; // the deviation is responsible for giving the frisbee a curve in it's flight path

        // Depending on the rotation of the frisbee the strength of the curve will be increased/decreased
        m_curveStrength = transform.localRotation.eulerAngles.z;
        if (m_deviation < 0)
            m_curveStrength = m_curveStrength > 180 ? m_curveStrength - 360 : m_curveStrength; // gets the angle between [-180, 180]
        else
            m_curveStrength = m_curveStrength > 180 ? -(m_curveStrength - 360) : -m_curveStrength; // gets the negative of the angle between [-180, 180]

        m_curveStrength = 3 + (m_curveStrength / 180 * 5);
        m_active = true;

        // Activate trails
        foreach (TrailRenderer trail in m_trails)
        {
            trail.gameObject.SetActive(true);
        }
        // Activate particles
        foreach (ParticleSystem particle in m_particles)
        {
            particle.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Increases the score and combo on a goal
    /// </summary>
    public void OnGoal()
    {
        m_score++;
        m_combo++;
    }

    /// <summary>
    /// Ends the combo and increases the misses on reset
    /// </summary>
    public void OnReset()
    {
        m_combo = 0;
        m_misses++;
    }

    /// <summary>
    /// Disables the trails and particles when the frisbee is placed back in hand
    /// </summary>
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
