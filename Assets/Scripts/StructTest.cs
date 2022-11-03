using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestClass8bytes
{
	public float a;
	public float b;
}

public struct TestStruct8bytes
{
    public float a;
    public float b;
}

public struct TestStruct64bytes
{
	public float a;
	public float b;
	public float c;
	public float d;
	public float e;
	public float f;
	public float g;
	public float h;
	public float i;
	public float j;
	public float k;
	public float l;
	public float m;
	public float n;
	public float o;
	public float p; // 64 byte
}

public struct TestStruct68bytes
{
	public float a;
	public float b;
	public float c;
	public float d;
	public float e;
	public float f;
	public float g;
	public float h;
	public float i;
	public float j;
	public float k;
	public float l;
	public float m;
	public float n;
	public float o;
	public float p; // 64 byte

	public float q; // 68 bytes
}

public class StructTest : MonoBehaviour
{
	public Text resultText;

    int arrayLength = 64;

	List<TestStruct8bytes> structList8bytes;
	TestStruct8bytes[] structArray8bytes;

	List<TestStruct68bytes> structList68bytes;
	TestStruct68bytes[] structArray68bytes;

	List<TestClass8bytes> classList8bytes;
	TestClass8bytes[] classArray8bytes;

	private void Awake()
    {
		structArray8bytes = new TestStruct8bytes[arrayLength];
		structList8bytes = new List<TestStruct8bytes>(arrayLength);
		structArray68bytes = new TestStruct68bytes[arrayLength];
		structList68bytes = new List<TestStruct68bytes>(arrayLength);
        for (int i = 0; i < arrayLength; i++)
        {
            TestStruct8bytes tempStruct8bytes;
            tempStruct8bytes.a = Random.value;
            tempStruct8bytes.b = Random.value;
            structList8bytes.Add(tempStruct8bytes);
            structArray8bytes[i] = tempStruct8bytes;

			TestStruct68bytes tempStruct68bytes;
			tempStruct68bytes.a = Random.value;
			tempStruct68bytes.b = Random.value;
			tempStruct68bytes.c = Random.value;
			tempStruct68bytes.d = Random.value;
			tempStruct68bytes.e = Random.value;
			tempStruct68bytes.f = Random.value;
			tempStruct68bytes.g = Random.value;
			tempStruct68bytes.h = Random.value;
			tempStruct68bytes.i = Random.value;
			tempStruct68bytes.j = Random.value;
			tempStruct68bytes.k = Random.value;
			tempStruct68bytes.l = Random.value;
			tempStruct68bytes.m = Random.value;
			tempStruct68bytes.n = Random.value;
			tempStruct68bytes.o = Random.value;
			tempStruct68bytes.p = Random.value;
			tempStruct68bytes.q = Random.value;
			structList68bytes.Add(tempStruct68bytes);
			structArray68bytes[i] = tempStruct68bytes;
        }
    }

	// Start is called before the first frame update
	public void RunTest()
	{
		string result = "";

        float length = arrayLength;
        float time1;

        time1 = Time.realtimeSinceStartup;
        for(int i = 0; i < 1000000; i++)
        {
            for(int j = 0; j < length; j++)
            {
                TestStruct8bytes tempStruct = structList8bytes[j];
                tempStruct.a += tempStruct.b;
                structList8bytes[j] = tempStruct;
            }
        }
		result += string.Format("Struct1 add list variables {0}", Time.realtimeSinceStartup - time1) + "\n";

        time1 = Time.realtimeSinceStartup;
        for (int i = 0; i < 1000000; i++)
        {
            for (int j = 0; j < length; j++)
            {
                structArray8bytes[j].a += structArray8bytes[j].b;
            }
        }
		result += string.Format("Struct1 add array variables {0}", Time.realtimeSinceStartup - time1) + "\n";

		time1 = Time.realtimeSinceStartup;
		for (int i = 0; i < 1000000; i++)
		{
			for (int j = 0; j < length; j++)
			{
				TestStruct68bytes tempStruct = structList68bytes[j];
				tempStruct.a += structList68bytes[j].b;
				structList68bytes[j] = tempStruct;
			}
		}
		result += string.Format("Struct2 add list variables {0}", Time.realtimeSinceStartup - time1) + "\n";

		time1 = Time.realtimeSinceStartup;
		for (int i = 0; i < 1000000; i++)
		{
			for (int j = 0; j < length; j++)
			{
				structArray68bytes[j].a += structArray68bytes[j].b;
			}
		}
		result += string.Format("Struct2 add array variables {0}", Time.realtimeSinceStartup - time1) + "\n";

		resultText.text = result;

	}
}
