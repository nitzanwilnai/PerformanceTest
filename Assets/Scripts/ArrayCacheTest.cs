using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrayCacheTest : MonoBehaviour
{
    public TextMeshProUGUI ResultText;

    // Start is called before the first frame update
    void Start()
    {
        arrayCacheTest();
    }

    void arrayCacheTest()
    {
        ResultText.text += "Array Cache Test\n";

        int numIterations = 1000000;

        int size = 1024;

        int[] array1 = new int[size];
        int[] array2 = new int[size];
        int[] array3 = new int[size];

        for(int i = 0; i < size; i++)
            array1[i] = i;

        for (int i = 0; i < size; i++)
            array2[i] = i;

        for (int i = size-1; i >= 0; i--)
            array3[i] = i;

        double timer1 = 0.0f;
        double timer2 = 0.0f;
        double timer3 = 0.0f;
        double time = 0.0f;

        for(int i = 0; i < numIterations; i++)
        {
            time = Time.realtimeSinceStartupAsDouble;
            for (int j = 0; j < size; j++)
                array1[j]++;
            timer1 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = size-1; j >= 0; j--)
                array2[j]++;
            timer2 += Time.realtimeSinceStartupAsDouble - time;

            time = Time.realtimeSinceStartupAsDouble;
            for (int j = size-1; j >= 0; j--)
                array3[j]++;
            timer3 += Time.realtimeSinceStartupAsDouble - time;
        }

        ResultText.text += "Array init forward run forward " + timer1.ToString("N4") + "\n";
        ResultText.text += "Array init forward run backward " + timer2.ToString("N4") + "\n";
        ResultText.text += "Array init backward run backward " + timer3.ToString("N4") + "\n";
    }
}
