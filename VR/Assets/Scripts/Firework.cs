using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    public ParticleSystem m_thrust;
    public ParticleSystem m_explosion;
    public Renderer m_renderer;

    public float m_length;
    public float m_speed;

    float m_timer;
    Vector3 m_startPos;
    bool m_exploded;
    
    void Awake()
    {
        m_startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_timer < m_length) // while the timer is still going
        {
            m_timer += Time.deltaTime;
            transform.position += new Vector3(0, m_speed, 0) * Time.deltaTime; // move the firework linearly upwards
        }
        else if (m_exploded && m_explosion.isStopped) // if the firework has exploded
        {
            gameObject.SetActive(false);
        }
        else if (!m_exploded) // if the timer is finished
        {
            m_thrust.Stop(); // stop the thrust particles 
            m_renderer.enabled = false; // disable the body
            m_explosion.Play(); // play the explosion
            m_exploded = true;
        }            
    }

    /// <summary>
    /// Resets the firework
    /// </summary>
    private void OnEnable()
    {
        transform.position = m_startPos;
        m_renderer.enabled = true;
        m_timer = 0;
        m_exploded = false;
        m_thrust.Play();
    }
}
