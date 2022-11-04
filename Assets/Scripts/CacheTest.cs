using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential, Pack = 0)]
public class IntObject
{
    public int A;
    public int B;
    public int C;
    public int D;
    public int E;
    public int F;
    public int G;
    public int H;
    public int I;
    public int J;
    public int K;
    public int L;
    public int M;
    public int N;
    public int O;
    public int P;

    public int A2;
    public int B2;
    public int C2;
    public int D2;
    public int E2;
    public int F2;
    public int G2;
    public int H2;
    public int I2;
    public int J2;
    public int K2;
    public int L2;
    public int M2;
    public int N2;
    public int O2;
    public int P2;
}

public class ArrayOfIntObjects
{
    public IntObject[] IntObjects;

    public ArrayOfIntObjects()
    {
        IntObjects = new IntObject[1024];
        for (int i = 0; i < 1024; i++)
            IntObjects[i] = new IntObject();
    }
}

public class MyArray
{
    public int[] Array;

    public MyArray()
    {
        Array = new int[1024];
    }
}

public class MyArrayGetSet
{
    public int[] Array { get; set; }

    public MyArrayGetSet()
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
    List<int> list = new List<int>();

    public static int SIZE = 1024;

    MyArray myArray = new MyArray();
    MyArrayGetSet myArrayGetSet = new MyArrayGetSet();
    SafeArray safeArray = new SafeArray();
    UnsafeArray unsafeArray = new UnsafeArray();
    ArrayOfIntObjects arrayOfIntObjects = new ArrayOfIntObjects();

    // Start is called before the first frame update
    void Start()
    {
        myArray.Array = new int[SIZE];
        myArrayGetSet.Array = new int[SIZE];
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

        double timer1 = 0.0f;
        double timer2 = 0.0f;
        double timer3 = 0.0f;
        double timer4 = 0.0f;
        double timer5 = 0.0f;
        double timer6 = 0.0f;
        double timer7 = 0.0f;
        double timer8 = 0.0f;
        double timer9 = 0.0f;
        double timer10 = 0.0f;
        double time = 0.0f;

        for (int j = 0; j < SIZE; j++)
        {
            localArray[j] = j;
            memberArray[j] = j;
            stackArray[j] = j;
            myArray.Array[j] = j;
            myArrayGetSet.Array[j] = j;
            safeArray[j] = j;
            unsafeArray[j] = j;
            list.Add(j);
            arrayOfIntObjects.IntObjects[j].A = j;
        }

        for (int i = 0; i < numIterations; i++)
        {
            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                localArray[j]++;
            timer1 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                memberArray[j]++;
            timer2 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                stackArray[j]++;
            timer3 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartup;
            for (int j = 0; j < SIZE; j++)
                myArray.Array[j]++;
            timer4 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                safeArray[j]++;
            timer5 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                unsafeArray[j]++;
            timer6 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                myArrayGetSet.Array[j]++;
            timer7 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                list[j]++;
            timer8 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                arrayOfIntObjects.IntObjects[j].A++;
            timer9 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < SIZE; j++)
                if(j < SIZE)
                    arrayOfIntObjects.IntObjects[j].A++;
            timer10 += Time.realtimeSinceStartupAsDouble - time;
        }

        ResultText.text += "Local Array " + timer1.ToString("N4") + "\n";
        ResultText.text += "Member Array " + timer2.ToString("N4") + "\n";
        ResultText.text += "Stack Array " + timer3.ToString("N4") + "\n";
        ResultText.text += "Object Member Array " + timer4.ToString("N4") + "\n";
        ResultText.text += "Safe Member Array " + timer5.ToString("N4") + "\n";
        ResultText.text += "UnSafe Member Array " + timer6.ToString("N4") + "\n";
        ResultText.text += "Object Member Array GetSet " + timer7.ToString("N4") + "\n";
        ResultText.text += "List " + timer8.ToString("N4") + "\n";
        ResultText.text += "Array of Int Objects " + timer9.ToString("N4") + "\n";
        ResultText.text += "Array of Int Objects with Branch " + timer10.ToString("N4") + "\n";
    }

    void TestMember()
    {
        for (int j = 0; j < SIZE; j++)
            memberArray[j]++;
    }

    void TestMyArrayGetSet()
    {
        for (int j = 0; j < SIZE; j++)
            myArrayGetSet.Array[j]++;
    }

    void TestMyArray()
    {
        for (int j = 0; j < SIZE; j++)
            myArray.Array[j]++;
    }

    void TestSafeArray()
    {
        for (int j = 0; j < SIZE; j++)
            safeArray[j]++;
    }

    void TestUnSafeArray()
    {
        for (int j = 0; j < SIZE; j++)
            unsafeArray[j]++;
    }

    void TestList()
    {
        for (int j = 0; j < SIZE; j++)
            list[j]++;
    }
}
