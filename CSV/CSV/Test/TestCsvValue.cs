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
    }
}
