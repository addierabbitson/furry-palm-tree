using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerRaycast : MonoBehaviour
{
    public bool m_validHit;
    public RaycastHit m_lastHit;
    public LineRenderer m_laser;

    public Vector3 PointDirection
    {
        get { return transform.forward; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OVRInput.Update();

        if (Physics.Raycast(transform.position, transform.forward, out m_lastHit))
        {
            m_validHit = true;
            m_laser.SetPosition(0, transform.position);
            m_laser.SetPosition(1, m_lastHit.point);
        }
        else
        {
            m_validHit = false;
            m_laser.SetPosition(0, transform.position);
            m_laser.SetPosition(1, transform.forward * 100);
        }
    }
}
