using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Com.Alking.CSV;
using NUnit.Framework;

namespace Edu.Test.CSV
{
    class TestCsvValue
    {
        [Test]
        public void TestImplicit()
        {
            int oriInt = 100;
            CsvValue v = oriInt;
            int resultInt = v;
            Assert.AreEqual(oriInt,resultInt);

            long oriLong = 10000000000000L;
            v = oriLong;
            long resultLong = v;
            Assert.AreEqual(oriLong,resultLong);

            bool oriBool = true;
            v = oriBool;
            bool resultBool = v;
            Assert.AreEqual(oriBool,resultBool);

            float oriF = 0.2f;
            v = oriF;
            float resultF = v;
            Assert.AreEqual(oriF,resultF);

            double oriD = 0.001d;
            v = oriD;
            double resultD = v;
            Assert.AreEqual(oriD,resultD);


        }

        [Test]
        public void TestBoolArray()
        {
            bool[] array = new bool[]{true,false,true,false};//count = 4
            CsvValue value = array;
            Assert.AreEqual(CsvValue.CsvValueType.ArrayBool,value.Type);
            bool[] result = value;
            Assert.AreEqual(4,result.Length);
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i],result[i]);
            }
        }
        [Test]
        public void TestIntArray()
        {
            int[] array = new int[] {1,2,3,4,5,6,7,8,9,10};
            CsvValue value = array;
            Assert.AreEqual(CsvValue.CsvValueType.ArrayInt, value.Type);
            int[] result = value;
            Assert.AreEqual(10, result.Length);
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i], result[i]);
            }
        }

        [Test]
        public void TestFloatArray()
        {
            float[] array = {1.1f,1.2f,1.3f,1.4f,1.5f};
            CsvValue value = array;
            Assert.AreEqual(CsvValue.CsvValueType.ArrayFloat, value.Type);
            float[] result = value;
            Assert.AreEqual(5, result.Length);
            for (int i = 0; i < array.Length; i++)
            {
                AssertAreEqual(array[i], result[i]);
            }
        }
        [Test]
        public void TestStringArray()
        {
            string[] array = new string[]{"a","b","c","d","e"};
            CsvValue value = array;
            Assert.AreEqual(CsvValue.CsvValueType.ArrayString, value.Type);
            string[] result = value;
            Assert.AreEqual(5, result.Length);
            for (int i = 0; i < array.Length; i++)
            {
                Assert.AreEqual(array[i], result[i]);
            }
        }

        [Test]
        public void TestEquals()
        {
            CsvValue[] array = new CsvValue[]
            {
                100,
                100L,
                100f,
                100d
            };
            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length; j++)
                {
                    AssertAreEqual(array[i],array[j]);
                }
            }

            AssertAreEqual(true,true);
            AssertAreEqual("hello","hello");
        }

        public void AssertAreEqual(CsvValue v1, CsvValue v2)
        {
            bool equal = v1.Equals(v2);
            Assert.True(equal);
        }

        [Test]
        public void TestConstrcutorInt()
        {
            string src = "";
            CsvValue value = new CsvValue(CsvValue.CsvValueType.Int,src);
            int v = value;
            Assert.AreEqual(0,v);
            src = "1234";
            value = new CsvValue(CsvValue.CsvValueType.Int, src);
            v = value;
            Assert.AreEqual(1234, v);
        }

        [Test]
        public void TestConstrcutorString()
        {
            string src = "hello";
            CsvValue value = new CsvValue(CsvValue.CsvValueType.String, src);
            string v = value;
            Assert.AreEqual(src, v);
        }

        [Test]
        public void TestConstrcutorArray()
        {
            string src = "1;2;3;4;5";
            CsvValue value = new CsvValue(CsvValue.CsvValueType.ArrayInt, src);
            int[] intArray = value;
            Assert.AreEqual(5,intArray.Length);
            
            Assert.AreEqual(1,intArray[0]);
            Assert.AreEqual(2,intArray[1]);
            Assert.AreEqual(3,intArray[2]);
            Assert.AreEqual(4,intArray[3]);
            Assert.AreEqual(5,intArray[4]);

            src = "str0;str1;str2;str3;str4";
            value = new CsvValue(CsvValue.CsvValueType.ArrayString, src);
            string[] strArray = value;
            for (int i = 0; i < 5; i++)
            {
                Assert.AreEqual("str" + i,strArray[i]);
            }
        }
    }
}
