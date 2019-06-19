using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingGoal : MonoBehaviour
{
    public Vector3 m_start;
    public Vector3 m_end;
    public float m_speed;

    float m_timer;
    bool m_direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // counts up or down depending on the direction
        if (m_direction)
            m_timer += Time.deltaTime;
        else
            m_timer -= Time.deltaTime;

        // swaps the direction when the timer finishes
        if (m_timer >= m_speed)
            m_direction = false;
        if (m_timer <= 0)
            m_direction = true;

        transform.position = Vector3.Lerp(m_start, m_end, m_timer / m_speed); // lerp the position between the start and end positions
    }
}
