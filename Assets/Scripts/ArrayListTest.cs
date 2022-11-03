using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayListTest : MonoBehaviour
{
    public Text resultText;

    enum TEST_STATE { NONE, ADD_TEST, ACCESS_TEST, DONE };
    TEST_STATE m_testState = TEST_STATE.NONE;

    int m_size;

    private void Awake()
    {
    }

    private void Start()
    {
        m_testState = TEST_STATE.ADD_TEST;
        resultText.text = "Starting Array vs List Test\n";
        m_size = 64;
    }

    public static void AddTest(int size, Text resultText, string testName)
    {
        List<int> list1 = new List<int>();
        List<int> list2 = new List<int>();

        int[] array1 = new int[size];
        int[] array2 = new int[size];
        for (int i = 0; i < size; i++)
        {
            list1.Add((int)(Random.value * int.MaxValue));
            list2.Add((int)(Random.value * int.MaxValue));
            array1[i] = ((int)(Random.value * int.MaxValue));
            array2[i] = ((int)(Random.value * int.MaxValue));
        }
        float time1 = 0.0f;
        float time2 = 0.0f;
        while(time1 < 1.0f && time2 < 1.0f)
        {
            float time = Time.realtimeSinceStartup;
            for (int i = 0; i < size; i++)
            {
                list1[i] += list2[i];
            }
            time1 += (Time.realtimeSinceStartup - time);

            time = Time.realtimeSinceStartup;
            for (int i = 0; i < size; i++)
            {
                array1[i] += array2[i];
            }
            time2 += (Time.realtimeSinceStartup - time);
        }

        resultText.text += testName;
        resultText.text += "Adding two lists of size " + size + ":\n" + time1 + " sec\n";
        resultText.text += "Adding two arrays of size " + size + ":\n " + time2 + "sec\n";
    }

    public static void AccessTest(int size, Text resultText)
    {

        List<int> list = new List<int>(size);
        int[] array = new int[size];

        for (int i = 0; i < size; i++)
        {
            list.Add((int)(Random.value * int.MaxValue));
            array[i] = ((int)(Random.value * int.MaxValue));
        }

        float time1 = 0.0f;
        float time2 = 0.0f;
        while (time1 < 1.0f && time2 < 1.0f)
        {
            int a = 0;
            float time = Time.realtimeSinceStartup;
            for (int i = 0; i < size; i++)
            {
                a += list[i];
            }
            time1 += (Time.realtimeSinceStartup - time);

            time = Time.realtimeSinceStartup;
            for (int i = 0; i < size; i++)
            {
                a += array[i];
            }
            time2 += (Time.realtimeSinceStartup - time);
        }

        resultText.text += "Adding two lists of size " + size + ":\n" + time1 + " sec\n";
        resultText.text += "Adding two arrays of size " + size + ":\n " + time2 + "sec\n";
    }

    private void Update()
    {
        switch (m_testState)
        {
            case TEST_STATE.ADD_TEST:
                {
                    AddTest(1024, resultText, "List not preallocated to size " + 1024);
                    m_testState = TEST_STATE.ACCESS_TEST;

                }
                break;
            case TEST_STATE.ACCESS_TEST:
                {
                    m_testState = TEST_STATE.DONE;
                }
                break;
            case TEST_STATE.DONE:
                {
                    resultText.text += "\nDONE\n";
                    m_testState = TEST_STATE.NONE;
                }
                break;
        }
    }

}