using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using NUnit.Framework;
using LumenWorks.Framework.IO.Csv;
namespace Edu.Test.CSV
{
    class CsvReaderDemo
    {
        [Test]
        public void Test01()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("key1,key2,key3");
            sb.AppendLine("#this is comment");
            sb.AppendLine("v11,v12,\"v13,\n\"");
            sb.AppendLine("v21,v22,\"v23\"");
            sb.AppendLine("v31,v32,\"v33\"");
            sb.AppendLine("v41,v42,v43");
            TextReader textReader = new StringReader(sb.ToString());
            CsvReader reader = new CsvReader(textReader,true);
            int fieldCount = reader.FieldCount;
            string[] headers = reader.GetFieldHeaders();
            List<int> indexs = new List<int>();

            foreach (string[] strings in reader)
            {
                string raw = reader.GetCurrentRawData();
            }
            
          

        }

        public static string CsvReader2String(CsvReader reader)
        {
            StringBuilder sb = new StringBuilder();

            Type type = reader.GetType();
            FieldInfo[] fields = type.GetFields();
            foreach (FieldInfo field in fields)
            {
                sb.Append(field.Name).Append(":").Append(field.GetValue(reader)).Append(",");
            }
            return sb.ToString();
        }
    }
}
