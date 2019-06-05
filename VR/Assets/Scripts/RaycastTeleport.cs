using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastTeleport : MonoBehaviour
{
    public Transform m_base;
    public ControllerRaycast m_raycast;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Back))
        {
            if (m_raycast.m_validHit)
            {
                m_base.position = m_raycast.m_lastHit.point;
            }
        }
    }
}
