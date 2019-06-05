using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public Transform m_controller;
    public float m_speed;

    Rigidbody m_rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 movement = ControllerJoystick();
        if (movement.x > 10 || movement.y > 10)
        {
            m_rigidbody.AddTorque(new Vector3(movement.x, 0, movement.y));
        }

        if (OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger))
        {
            m_rigidbody.AddForce(transform.forward * m_speed);
        }
    }

    Vector2 ControllerJoystick()
    {
        Vector2 movement;

        float pitch = Vector3.Angle(m_controller.forward, transform.up);

        float roll = Vector3.Angle(m_controller.right, transform.right);

        movement = new Vector2(pitch , roll);

        return movement;
    }
}
