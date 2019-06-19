using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeRelease : MonoBehaviour
{
    public Frisbee m_frisbee;
    public Transform m_anchor;
    public Transform m_controller;
    public float m_releaseThreshold;
    public bool m_test;
    public float m_shootStrength;
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

        if ((m_inHand && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) || (m_inHand && m_test))
        {            
            Release();
        }
        else if ((!m_inHand && OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger)) || (!m_inHand && m_test))
        {
            PlaceInHand();
            m_frisbee.OnReset();
        }

        m_lastPos = m_frisbee.transform.position;
    }

    public void Release()
    {
        Vector3 newVelocity;
        if (m_controllerMovement)
        {
            float forwardSpeed = m_frisbee.transform.position.z - m_lastPos.z;
            float sideSpeed = m_frisbee.transform.position.x - m_lastPos.x;

            newVelocity = new Vector3(sideSpeed / 2, 0, forwardSpeed * 2) / Time.deltaTime;            
        }
        else
        {
            newVelocity = m_controller.forward * m_shootStrength;
        }

        if (newVelocity.sqrMagnitude >= m_releaseThreshold * m_releaseThreshold && newVelocity.z > 0)
        {
            m_frisbeeRigidbody.velocity = newVelocity;
            m_frisbee.m_initialVelocity = newVelocity;

            m_frisbee.transform.parent = null;
            m_frisbeeRigidbody.isKinematic = false;

            m_frisbee.m_trail.gameObject.SetActive(true);
            m_frisbee.OnThrow();

            m_inHand = false;
            m_test = false;
        }
    }

    public void PlaceInHand()
    {
        m_frisbeeRigidbody.isKinematic = true;
        m_frisbeeRigidbody.velocity = Vector3.zero;

        m_frisbee.transform.parent = m_anchor;
        m_frisbee.transform.localPosition = m_startPos;
        m_frisbee.transform.localRotation = Quaternion.identity;

        m_frisbee.m_trail.gameObject.SetActive(false);
        m_frisbee.m_trail.Clear();

        m_inHand = true;
        m_test = false;
    }
}
