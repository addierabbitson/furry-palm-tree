using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastGrab : MonoBehaviour
{
    public float m_minDistance;
    public float m_maxDistance;
    public float m_distanceIncrement;
    public float m_strength;
    public ControllerRaycast m_raycast;

    public Rigidbody m_target;
    float m_distance;

    // Start is called before the first frame update
    void Start()
    {
        m_distance = m_minDistance;
    }

    // Update is called once per frame
    void Update()
    {
        if (m_target)
        {
            Vector3 targetPos = m_raycast.transform.position + m_raycast.transform.forward * m_distance;
            if ((targetPos - m_target.position).magnitude > 0.1)
            {
                m_target.AddForce((targetPos - m_target.position).normalized * m_strength);
            }

            if (!OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
            {
                m_target.useGravity = true;
                m_target.drag = 0;
                m_target = null;
            }
           
            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {
                Vector2 direction = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);

                if (Mathf.Abs(direction.y) > Mathf.Abs(direction.x))
                {
                    if (direction.y > 0)
                        m_distance += m_distanceIncrement;
                    else
                        m_distance -= m_distanceIncrement;

                    m_distance = Mathf.Clamp(m_distance, m_minDistance, m_maxDistance);

                }
            }
        }
        else
        {
            if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) && m_raycast.m_validHit)
            {
                if (m_raycast.m_lastHit.rigidbody)
                {
                    m_target = m_raycast.m_lastHit.rigidbody;
                    m_target.useGravity = false;
                    m_target.drag = 1;
                }
            }
        }
    }
}
