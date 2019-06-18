using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_score;
    public TMPro.TextMeshProUGUI m_combo;
    public Frisbee m_frisbee;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_score.text = "Goals: " + m_frisbee.m_score.ToString();
        m_combo.text = "Combo: " + m_frisbee.m_combo.ToString();
    }
}
