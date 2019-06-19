using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrisbeeRelease : MonoBehaviour
{
    public Frisbee m_frisbee;
    public Transform m_anchor;
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

    /// <summary>
    /// Detatches the frisbee from the player and uses it's last position and current position to calculate the desired velocity
    /// </summary>
    public void Release()
    {
        Vector3 newVelocity;
        if (m_controllerMovement) // if using the controllers movement for the velocity
        {
            // getting delta movement on the x and z axis
            float forwardSpeed = m_frisbee.transform.position.z - m_lastPos.z;
            float sideSpeed = m_frisbee.transform.position.x - m_lastPos.x;

            newVelocity = new Vector3(sideSpeed / 2, 0, forwardSpeed * 2) / Time.deltaTime; 
        }
        else // DEBUG fires in a direction at a set strength
        {
            newVelocity = m_anchor.forward * m_shootStrength;
        }

        // The frisbee doesn't get released if the magnitude of the velocity is below the threshold or if it is going backwards
        if (newVelocity.sqrMagnitude >= m_releaseThreshold * m_releaseThreshold && newVelocity.z > 0)
        {
            m_frisbeeRigidbody.velocity = newVelocity;
            m_frisbee.InitialVelocity = newVelocity;

            m_frisbee.transform.parent = null;
            m_frisbeeRigidbody.isKinematic = false;

            m_frisbee.OnThrow();

            m_inHand = false;
            m_test = false;
        }
    }

    /// <summary>
    /// Places the frisbee back into the hand of the player
    /// </summary>
    public void PlaceInHand()
    {
        m_frisbeeRigidbody.isKinematic = true;
        m_frisbeeRigidbody.velocity = Vector3.zero;

        m_frisbee.transform.parent = m_anchor;
        m_frisbee.transform.localPosition = m_startPos;
        m_frisbee.transform.localRotation = Quaternion.identity;

        m_frisbee.OnInHand();

        m_inHand = true;
        m_test = false;
    }
}
