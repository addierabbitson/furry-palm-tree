using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeRelease : MonoBehaviour
{
    public GameObject m_frisbee;
    public Transform m_anchor;
    public float m_throwStrength;
    public float m_shootStrength;
    public bool m_test;
    public Transform m_controller;
    public bool m_controllerMovement;

    Rigidbody m_frisbeeRigidbody;
    Vector3 m_lastPos;
    Vector3 m_startPos;
    bool m_inHand = true;

    // Start is called before the first frame update
    void Start()
    {
        m_frisbeeRigidbody = m_frisbee.GetComponent<Rigidbody>();
        m_startPos = m_frisbee.transform.localPosition;       
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            m_controllerMovement = !m_controllerMovement;
        }

        if ((m_inHand && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) || (m_inHand && m_test))
        {
            m_frisbee.transform.parent = null;
            m_frisbeeRigidbody.isKinematic = false;
            if (m_controllerMovement)
            {
                m_frisbeeRigidbody.velocity = (m_frisbee.transform.position - m_lastPos) * m_throwStrength;
            }
            else
            {
                m_frisbeeRigidbody.velocity = m_controller.forward * m_shootStrength;
            }

            m_inHand = false;
            m_test = false; 
        }
        else if ((!m_inHand && OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)) || (!m_inHand && m_test))
        {
            m_frisbeeRigidbody.isKinematic = true;
            m_frisbeeRigidbody.velocity = Vector3.zero;

            m_frisbee.transform.parent = m_anchor;
            m_frisbee.transform.localPosition = m_startPos;

            m_inHand = true;
            m_test = false;
        }

        m_lastPos = m_frisbee.transform.position;
    }
}
