using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrayTest : MonoBehaviour
{
	public Text resultText;

    int arrayLength1 = 64;
	int arrayLength2 = 6400;

    float[] array1A;
    float[] array1B;

	float[] array2A;
	float[] array2B;

	private void Awake()
    {
        array1A = new float[arrayLength1];
        array1B = new float[arrayLength1];
        for (int i = 0; i < arrayLength1; i++)
        {
            array1A[i] = Random.value;
            array1B[i] = Random.value;
        }

		array2A = new float[arrayLength2];
		array2B = new float[arrayLength2];
		for (int i = 0; i < arrayLength2; i++)
		{
			array2A[i] = Random.value;
			array2B[i] = Random.value;
		}
	}

	// Start is called before the first frame update
	public void RunTest()
    {
		string result = "";

        int length = arrayLength1;
        float time;

		time = Time.realtimeSinceStartup;
		for (int i = 0; i < 1000000; i++)
		{
			for (int j = 0; j < array1A.Length; j++)
			{
				array1A[j] += array1B[j];
			}
		}
		result += string.Format("Add two arrays {0}", Time.realtimeSinceStartup - time) + "\n";

		time = Time.realtimeSinceStartup;
		for (int i = 0; i < 1000000; i++)
		{
			for (int j = 0; j < length; j++)
			{
				array1A[j] += array1B[j];
			}
		}
		result += string.Format("Add two arrays - cache array length {0}", Time.realtimeSinceStartup - time) + "\n";


		time = Time.realtimeSinceStartup;
		for (int i = 0; i < 1000000; i++)
		{
			for (int j = length - 1; j >= 0; j--)
			{
				array1A[j] += array1B[j];
			}
		}
		result += string.Format("Reverse Add two arrays {0}", Time.realtimeSinceStartup - time) + "\n";

		result += "Testing multiple array sizes, length cached:\n";
		result += RunArrayTest1(64, 1000000);
		result += RunArrayTest1(640, 100000);
		result += RunArrayTest1(6400, 10000);
		result += RunArrayTest1(64000, 1000);
		result += RunArrayTest1(640000, 100);
		result += RunArrayTest1(6400000, 10);
		result += RunArrayTest1(64000000, 1);

		resultText.text = result;

	}

	string RunArrayTest1(int arraySize, int numTests)
	{
		float[] array1 = new float[arraySize];
		float[] array2 = new float[arraySize];
		for (int i = 0; i < arraySize; i++)
		{
			array1[i] = Random.value;
			array2[i] = Random.value;
		}

		float time = Time.realtimeSinceStartup;
		for (int j = 0; j < numTests; j++)
		{
			for (int i = 0; i < arraySize; i++)
			{
				array1[i] += array2[i];
			}
		}
		return string.Format("Add two arrays of size {0} : {1}", arraySize, Time.realtimeSinceStartup - time) + "\n";
	}
}
