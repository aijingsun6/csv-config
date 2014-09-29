using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Com.Alking.CSV;
using NUnit.Framework;

namespace Edu.Test.CSV
{
    class TestCsvConfigReader
    {
        [Test]
        public void TestNormal()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S");//7types
            sb.AppendLine("key1,key2,key3,key4,key5,key6,key7");
            sb.AppendLine("1, true, 101, 1001,1.1, 2.1, hello world 1");
            sb.AppendLine("2,false, 102, 1002,1.2, 2.2, hello world 2");
            sb.AppendLine("3, true, 103, 1003,1.3, 2.3, hello world 3");
            sb.AppendLine("4,false, 104, 1004,1.4, 2.4, hello world 4");
            sb.AppendLine("5, true, 105, 1005,1.5, 2.5, hello world 5");

            CsvConfigReader reader = new CsvConfigReader();
            reader.Read(new StringReader(sb.ToString()));
            Assert.AreEqual(CsvValue.CsvValueType.Int,reader.KeyType);
            Assert.AreEqual(5,reader.Keys.Count);
            Assert.AreEqual(1, reader.Keys[0]);
            Assert.AreEqual(2, reader.Keys[1]);
            Assert.AreEqual(3, reader.Keys[2]);
            Assert.AreEqual(4, reader.Keys[3]);
            Assert.AreEqual(5, reader.Keys[4]);
            Assert.AreEqual(7,reader.FieldNames.Count);
            Assert.AreEqual("key1", reader.FieldNames[0]);
            Assert.AreEqual("key2", reader.FieldNames[1]);
            Assert.AreEqual("key3", reader.FieldNames[2]);
            Assert.AreEqual("key4", reader.FieldNames[3]); 
            Assert.AreEqual("key5", reader.FieldNames[4]);
            Assert.AreEqual("key6", reader.FieldNames[5]);
            Assert.AreEqual("key7", reader.FieldNames[6]);

            CsvConfig config = reader[reader.Keys[0]];

            Assert.AreEqual(0, config[0].Index);
            Assert.AreEqual(true, config[0].IsKey);
            string key = config[0].Name;
            Assert.AreEqual("key1", key);
            Assert.AreEqual(1, config[0].Value);
            Assert.AreEqual(config[0], config["key1"]);

            Assert.AreEqual(1, config[1].Index);
            Assert.AreEqual(false, config[1].IsKey);
            Assert.AreEqual("key2", config[1].Name);
            bool b = config[1].Value;
            Assert.AreEqual(true, b);
            Assert.AreEqual(config[1], config["key2"]);

            Assert.AreEqual(2, config[2].Index);
            Assert.AreEqual(false, config[2].IsKey);
            Assert.AreEqual("key3", config[2].Name);
            Assert.AreEqual(101, config[2].Value);
            Assert.AreEqual(config[2], config["key3"]);

            Assert.AreEqual(3, config[3].Index);
            Assert.AreEqual(false, config[3].IsKey);
            Assert.AreEqual("key4", config[3].Name);
            Assert.AreEqual(1001, config[3].Value);
            Assert.AreEqual(config[3], config["key4"]);

            Assert.AreEqual(4, config[4].Index);
            Assert.AreEqual(false, config[4].IsKey);
            Assert.AreEqual("key5", config[4].Name);
            float f = config[4].Value;
            Assert.AreEqual(float.Parse("1.1"), f);
            Assert.AreEqual(config[4], config["key5"]);

            Assert.AreEqual(5, config[5].Index);
            Assert.AreEqual(false, config[5].IsKey);
            Assert.AreEqual("key6", config[5].Name);
            double d = config[5].Value;
            Assert.AreEqual(double.Parse("2.1"), d);
            Assert.AreEqual(config[5], config["key6"]);

            Assert.AreEqual(6, config[6].Index);
            Assert.AreEqual(false, config[6].IsKey);
            Assert.AreEqual("key7", config[6].Name);
            string s = config[6].Value;
            Assert.AreEqual("hello world 1", s);
            Assert.AreEqual(config[6], config["key7"]);
        }

        [Test]
        public void TestKeyIsString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("I,B,I,L,F,D,KS");//7types
            sb.AppendLine("key1,key2,key3,key4,key5,key6,key7");
            sb.AppendLine("1, YES, 101, 1001,1.1, 2.1, k1");
            sb.AppendLine("2,NO, 102, 1002,1.2, 2.2, k2");
            sb.AppendLine("3, YES, 103, 1003,1.3, 2.3, k3");
            sb.AppendLine("4,NO, 104, 1004,1.4, 2.4, k4");
            sb.AppendLine("5, YES, 105, 1005,1.5, 2.5, k5");

            CsvConfigReader reader = new CsvConfigReader();
            reader.Read(new StringReader(sb.ToString()));
            Assert.AreEqual(CsvValue.CsvValueType.String, reader.KeyType);

            Assert.AreEqual(5,reader[reader.Keys[4]]["key1"].Value);

            Assert.AreEqual(5, reader.Keys.Count);
            string key;

            key = reader.Keys[0];
            Assert.AreEqual("k1", key);
            key = reader.Keys[1];
            Assert.AreEqual("k2", key);
            key = reader.Keys[2];
            Assert.AreEqual("k3", key);
            key = reader.Keys[3];
            Assert.AreEqual("k4", key);
            key = reader.Keys[4];
            Assert.AreEqual("k5", key);

          
            Assert.AreEqual(7, reader.FieldNames.Count);
            Assert.AreEqual("key1", reader.FieldNames[0]);
            Assert.AreEqual("key2", reader.FieldNames[1]);
            Assert.AreEqual("key3", reader.FieldNames[2]);
            Assert.AreEqual("key4", reader.FieldNames[3]);
            Assert.AreEqual("key5", reader.FieldNames[4]);
            Assert.AreEqual("key6", reader.FieldNames[5]);
            Assert.AreEqual("key7", reader.FieldNames[6]);

            CsvConfig config = reader[reader.Keys[0]];

            Assert.AreEqual(0, config[0].Index);
            Assert.AreEqual(false, config[0].IsKey);
            Assert.AreEqual("key1", config[0].Name);
            Assert.AreEqual(1, config[0].Value);
            Assert.AreEqual(config[0], config["key1"]);

            Assert.AreEqual(1, config[1].Index);
            Assert.AreEqual(false, config[1].IsKey);
            Assert.AreEqual("key2", config[1].Name);
            bool b = config[1].Value;
            Assert.AreEqual(true, b);
            Assert.AreEqual(config[1], config["key2"]);

            Assert.AreEqual(2, config[2].Index);
            Assert.AreEqual(false, config[2].IsKey);
            Assert.AreEqual("key3", config[2].Name);
            Assert.AreEqual(101, config[2].Value);
            Assert.AreEqual(config[2], config["key3"]);

            Assert.AreEqual(3, config[3].Index);
            Assert.AreEqual(false, config[3].IsKey);
            Assert.AreEqual("key4", config[3].Name);
            Assert.AreEqual(1001, config[3].Value);
            Assert.AreEqual(config[3], config["key4"]);

            Assert.AreEqual(4, config[4].Index);
            Assert.AreEqual(false, config[4].IsKey);
            Assert.AreEqual("key5", config[4].Name);
            float f = config[4].Value;
            Assert.AreEqual(float.Parse("1.1"), f);
            Assert.AreEqual(config[4], config["key5"]);

            Assert.AreEqual(5, config[5].Index);
            Assert.AreEqual(false, config[5].IsKey);
            Assert.AreEqual("key6", config[5].Name);
            double d = config[5].Value;
            Assert.AreEqual(double.Parse("2.1"), d);
            Assert.AreEqual(config[5], config["key6"]);

            Assert.AreEqual(6, config[6].Index);
            Assert.AreEqual(true, config[6].IsKey);
            Assert.AreEqual("key7", config[6].Name);
            string s = config[6].Value;
            Assert.AreEqual("k1", s);
            Assert.AreEqual(config[6], config["key7"]);
        }

        [Test]
        public void TestTypeError()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S,DD");//DD is error
            CsvConfigReader reader = new CsvConfigReader();
            bool hasException = false;
            Exception e = null;
            try
            {
                reader.Read(new StringReader(sb.ToString()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                hasException = true;
                e = exception;
            }
            Assert.True(hasException);
            Assert.True(e is CsvException);
            
        }

        [Test]
        public void TestDoubleKeyType()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S,KL");//7types
            CsvConfigReader reader = new CsvConfigReader();
            bool hasException = false;
            Exception e = null;
            try
            {
                reader.Read(new StringReader(sb.ToString()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                hasException = true;
                e = exception;
            }
            Assert.True(hasException);
            Assert.True(e is CsvException);
        }
        [Test]
        public void TestFieldNumberError()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S");//7types
            sb.AppendLine("key1,key2");
            CsvConfigReader reader = new CsvConfigReader();
            bool hasException = false;
            Exception e = null;
            try
            {
                reader.Read(new StringReader(sb.ToString()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                hasException = true;
                e = exception;
            }
            Assert.True(hasException);
            Assert.True(e is CsvException);
        }

        [Test]
        public void TestTypeNotMatchError()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("KI,B,I,L,F,D,S");//7types
            sb.AppendLine("key1,key2,key3,key4,key5,key6,key7");
            sb.AppendLine("1, 112, 101, 1001,1.1, 2.1, hello world 1");
            CsvConfigReader reader = new CsvConfigReader();
            bool hasException = false;
            Exception e = null;
            try
            {
                reader.Read(new StringReader(sb.ToString()));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                hasException = true;
                e = exception;
            }
            Assert.True(hasException);
            Assert.True(e is CsvException);
        }

        [Test]
        public void TestFileRead()
        {
            string path = Environment.CurrentDirectory + "/config.csv";
            if (!File.Exists(path))
            {
                throw new Exception("file not found");
            }
            CsvConfigReader reader = new CsvConfigReader();
            reader.ReadFile(path);
            Assert.AreEqual(7,reader.FieldNames.Count);
            Assert.AreEqual("key",reader.FieldNames[0]);
            Assert.AreEqual("nameBool", reader.FieldNames[1]);
            Assert.AreEqual("nameInt", reader.FieldNames[2]);
            Assert.AreEqual("nameLong", reader.FieldNames[3]);
            Assert.AreEqual("nameFloat", reader.FieldNames[4]);
            Assert.AreEqual("nameDouble", reader.FieldNames[5]);
            Assert.AreEqual("nameString", reader.FieldNames[6]);
            Assert.AreEqual(100,reader.Keys.Count);
            Assert.AreEqual(CsvValue.CsvValueType.Int,reader.KeyType);
            for (int i = 1; i <= 100; i++)
            {
                if (i <26 || (i > 50 && i < 76))
                {
                    Assert.True(reader[i]["nameBool"].Value);
                }
                else
                {
                    Assert.False(reader[i]["nameBool"].Value);
                }
                Assert.AreEqual(100 + i,reader[i]["nameInt"].Value);
                Assert.AreEqual(1000 + i, reader[i]["nameLong"].Value);
                Assert.AreEqual(0 + 0.01f * i, reader[i]["nameFloat"].Value,0.00001f);
                Assert.AreEqual(1 + 0.01f * i, reader[i]["nameDouble"].Value, 0.00001f);
                string s = i < 10 ? "a0" + i :  "a" + i;
                Assert.AreEqual(s, reader[i]["nameString"].Value.Value);//need implicit to string
                
            }

        }
    }

}
