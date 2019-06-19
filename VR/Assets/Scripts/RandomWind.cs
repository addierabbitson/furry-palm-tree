using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWind : MonoBehaviour
{
    public Wind[] m_windFields;
    public float m_minStrength;
    public float m_maxStrength;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            Randomize();
        }
    }

    void Randomize()
    {
        foreach (Wind wind in m_windFields)
        {
            bool active = Random.Range(0, 2) != 0;

            if (active)
            {
                float xStrength = Random.Range(m_minStrength, m_maxStrength);
                float zStrength = Random.Range(m_minStrength, m_maxStrength);

                wind.m_windStrength = new Vector3(xStrength, 0, zStrength);
            }
            else
            {
                wind.gameObject.SetActive(false);
            }
        }
    }
}
