using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Com.Alking.CSV;
using NUnit.Framework;

namespace Edu.Test.CSV
{
    class CsvConfigReaderSample
    {
        /// <summary>
        /// key type is string
        /// </summary>
        [Test]
        public void Test1()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("I,B,I,L,F,D,KS");//7types
            sb.AppendLine("key1,key2,key3,key4,key5,key6,key7");
            sb.AppendLine("1, true, 101, 1001,1.1, 2.1, k1");
            sb.AppendLine("2,false, 102, 1002,1.2, 2.2, k2");
            sb.AppendLine("3, true, 103, 1003,1.3, 2.3, k3");
            sb.AppendLine("4,false, 104, 1004,1.4, 2.4, k4");
            sb.AppendLine("5, true, 105, 1005,1.5, 2.5, k5");

            CsvConfigReader reader = new CsvConfigReader();
            reader.Read(new StringReader(sb.ToString()));
            //key type is string (KS in first line)
            //so keye is k1,k2,k3,k4,k5,5 elements

            //get 3th config
            CsvConfig config = reader["k3"];
            //get field key4 1003
            int value = config["key4"].Value;
            Assert.AreEqual(1003,value);
            value = config[3].Value;
            Assert.AreEqual(1003, value);

        }

        /// <summary>
        /// key type is int
        /// </summary>
        [Test]
        public void Test2()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S");//7types
            sb.AppendLine("key1,key2,key3,key4,key5,key6,key7");
            sb.AppendLine("1, true, 101, 1001,1.1, 2.1, k1");
            sb.AppendLine("2,false, 102, 1002,1.2, 2.2, k2");
            sb.AppendLine("3, true, 103, 1003,1.3, 2.3, k3");
            sb.AppendLine("4,false, 104, 1004,1.4, 2.4, k4");
            sb.AppendLine("5, true, 105, 1005,1.5, 2.5, k5");

            CsvConfigReader reader = new CsvConfigReader();
            reader.Read(new StringReader(sb.ToString()));
            //key type is string (KS in first line)
            //so keye is k1,k2,k3,k4,k5,5 elements
            for (int i = 1; i < 6; i++)
            {
                int key3Value = reader[i]["key3"].Value;
                Assert.AreEqual(100 + i,key3Value);

                long key4Value = reader[i]["key4"].Value;
                Assert.AreEqual(1000 + i, key4Value);

                string key7Value = reader[i]["key7"].Value;
                Assert.AreEqual("k" + i,key7Value);

            }
        }

        [Test]
        public void Test3()
        {
            Assert.AreEqual(1,1.0f);
        }
    }
}
