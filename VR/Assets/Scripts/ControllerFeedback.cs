using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerFeedback : MonoBehaviour
{
    public Transform m_touchCircle;
    public GameObject m_backButton;
    public GameObject m_trigger;
    public GameObject m_touchPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 touchPos = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad) * 0.02f;
        if (touchPos.sqrMagnitude != 0)
        {
            m_touchCircle.localPosition = new Vector3(touchPos.x, 0, touchPos.y);
            m_touchCircle.gameObject.SetActive(true);
        }
        else
        {
            m_touchCircle.gameObject.SetActive(false);
        }

        m_touchPress.SetActive(OVRInput.Get(OVRInput.Button.PrimaryTouchpad));

        m_backButton.SetActive(OVRInput.Get(OVRInput.Button.Back));

        m_trigger.SetActive(OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger));
    }
}
