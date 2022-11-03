using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class MyArray
{
    public int[] Array;

    public MyArray()
    {
        Array = new int[1024];
    }
}

public class UnsafeArray
{
    public int[] Array;

    public UnsafeArray()
    {
        Array = new int[1024];
    }

    public int this[int index]
    {
        get
        {
            return Array[index];
        }
        set
        {
            Array[index] = index;
        }
    }
}

public class SafeArray
{
    public int[] Array;

    public SafeArray()
    {
        Array = new int[1024];
    }

    public int this[int index]
    {
        get
        {
            if (index >= Array.Length)
                throw new Exception();
            return Array[index];
        }
        set
        {
            Array[index] = index;
        }
    }
}

public class CacheTest : MonoBehaviour
{
    public TextMeshProUGUI ResultText;

    int[] memberArray = new int[SIZE];

    public static int SIZE = 1024;

    MyArray myArray = new MyArray();
    SafeArray safeArray = new SafeArray();
    UnsafeArray unsafeArray = new UnsafeArray();

    // Start is called before the first frame update
    void Start()
    {
        myArray.Array = new int[SIZE];
        safeArray.Array = new int[SIZE];
        unsafeArray.Array = new int[SIZE];

        ResultText.text = "TestCache\n";
        TestCache();
    }

    unsafe void TestCache()
    {

        int numIterations = 100000;

        int[] localArray = new int[SIZE];
        int* stackArray = stackalloc int[SIZE];

        float timer1 = 0.0f;
        float timer2 = 0.0f;
        float timer3 = 0.0f;
        float timer4 = 0.0f;
        float timer5 = 0.0f;
        float timer6 = 0.0f;
        float time = 0.0f;

        for (int j = 0; j < SIZE; j++)
        {
            localArray[j] = j;
            myArray.Array[j] = j;
            stackArray[j] = j;
            memberArray[j] = j;
        }

        for (int i = 0; i < numIterations; i++)
        {
            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                localArray[j]++;
            timer1 += Time.realtimeSinceStartup - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                memberArray[j]++;
            timer2 += Time.realtimeSinceStartup - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                stackArray[j]++;
            timer3 += Time.realtimeSinceStartup - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                myArray.Array[j]++;
            timer4 += Time.realtimeSinceStartup - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                safeArray[j]++;
            timer5 += Time.realtimeSinceStartup - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                unsafeArray[j]++;
            timer6 += Time.realtimeSinceStartup - time;

        }

        ResultText.text += "Local Array " + timer1 + "\n";
        ResultText.text += "Member Array " + timer2 + "\n";
        ResultText.text += "Stack Array " + timer3 + "\n";
        ResultText.text += "Object Member Array " + timer4 + "\n";
        ResultText.text += "Safe Member Array " + timer5 + "\n";
        ResultText.text += "UnSafe Member Array " + timer6 + "\n";
    }
}
