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
        if (m_timer < m_length)
        {
            m_timer += Time.deltaTime;
            transform.position += new Vector3(0, m_speed, 0) * Time.deltaTime;
        }
        else if (m_exploded && m_explosion.isStopped)
        {
            gameObject.SetActive(false);
        }
        else if (!m_exploded)
        {
            m_thrust.Stop();
            m_renderer.enabled = false;
            m_explosion.Play();
            m_exploded = true;
        }            
    }

    private void OnEnable()
    {
        transform.position = m_startPos;
        m_renderer.enabled = true;
        m_timer = 0;
        m_exploded = false;
        m_thrust.Play();
    }
}
