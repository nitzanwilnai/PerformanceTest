using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ListGCTest : MonoBehaviour
{
    public TextMeshProUGUI ResultText;

    public int MaxSize = 1024;

    List<int> m_list = new List<int>();
    bool m_inTest = false;

    // Start is called before the first frame update
    void Start()
    {
        listGCTest();
    }

    void listGCTest()
    {
        m_inTest = true;
    }

    private void Update()
    {
        if (m_inTest)
        {
            if (m_list.Count < MaxSize)
                m_list.Add(1);
            else
                m_inTest = false;
        }

    }
}

//float m_time = 0.0f;
//m_time = Time.realtimeSinceStartup;
//ResultText.text = "List GC Test\n";
//ResultText.text += "Count " + m_list.Count + " Capacity " + m_list.Capacity + "\n";
//ResultText.text += "Done!\nTook " + (Time.realtimeSinceStartup - m_time) + " seconds\n";
