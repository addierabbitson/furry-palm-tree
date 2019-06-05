using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPush : MonoBehaviour
{
    public float m_forceStrength;
    public ControllerRaycast m_raycast;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {           
        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (m_raycast.m_validHit)
            {
                m_raycast.m_lastHit.rigidbody.AddForce(m_raycast.PointDirection * m_forceStrength);
            }
        }
        
    }
}
