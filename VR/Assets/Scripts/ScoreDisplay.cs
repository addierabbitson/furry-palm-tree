using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_text;
    public Frisbee m_frisbee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_text.text = m_frisbee.m_score.ToString() + ":" + m_frisbee.m_misses.ToString();
    }
}
