using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using LumenWorks.Framework.IO.Csv;

namespace Com.Alking.CSV
{
    /// <summary>
    /// csv config reader
    /// <example>
    /// read a file:
    /// <para>string path = Environment.CurrentDirectory + "/config.csv";   </para>
    /// <para>if (!File.Exists(path))                                       </para>
    /// <para>{                                                             </para>
    /// <para>    throw new Exception("file not found");                    </para>
    /// <para>}                                                             </para>
    /// <para><see cref="CsvConfigReader"/> reader = new <see cref="CsvConfigReader"/>();               </para>
    /// <para>reader.<see cref="ReadFile"/>(path);                                            </para>
    /// <para>you can get main key type: reader.<see cref="KeyType"/>       </para>
    /// <para>you can get your value :   reader.[index]["name"].Value;</para>
    /// </example>
    /// </summary>
    public class CsvConfigReader : IEnumerable<CsvConfig>
    {
        private readonly Dictionary<string,CsvValue.CsvValueType> _typeDic = new Dictionary<string, CsvValue.CsvValueType>
        {
            {"b",CsvValue.CsvValueType.Bool},
            {"i",CsvValue.CsvValueType.Int},
            {"l",CsvValue.CsvValueType.Long},
            {"f",CsvValue.CsvValueType.Float},
            {"d",CsvValue.CsvValueType.Double},
            {"s",CsvValue.CsvValueType.String},
        }; 


        private readonly Dictionary<string,CsvValue.CsvValueType> _keyTypeDic = new Dictionary<string, CsvValue.CsvValueType>
        {
            {"ki",CsvValue.CsvValueType.Int},
            {"kl",CsvValue.CsvValueType.Long},
            {"ks",CsvValue.CsvValueType.String},
        }; 

        private readonly Dictionary<CsvValue,CsvConfig> _configDic = new Dictionary<CsvValue, CsvConfig>();

        private CsvValue.CsvValueType _keyType = CsvValue.CsvValueType.None;
        /// <summary>
        /// main key type.
        /// can be int ,long ,string
        /// </summary>
        public CsvValue.CsvValueType KeyType
        {
            get { return _keyType; }
        }
        /// <summary>
        /// main key type
        /// you can use like this:
        /// int intKey = Keys[index];
        /// int longKey = Keys[index];
        /// string strKey = Keys[index];
        /// </summary>
        public List<CsvValue> Keys
        {
            get
            {
                List<CsvValue> list = new List<CsvValue>();
                foreach (CsvValue key in _configDic.Keys)
                {
                    list.Add(key);
                }
                return list;
            }
        }

        private readonly List<string> _fieldNames = new List<string>(); 
        /// <summary>
        /// it contains main keys you defined.
        /// </summary>
        public List<string> FieldNames
        {
            get { return _fieldNames; }
        } 

        /// <summary>
        /// if <see cref="KeyType"/>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public CsvConfig this[CsvValue key]
        {
            get
            {
                foreach (KeyValuePair<CsvValue, CsvConfig> pair in _configDic)
                {
                    if (pair.Key.Equals(key))
                    {
                        return pair.Value;
                    }
                }
                return null;
            }
        }
        public CsvConfigReader()
        {
           Reset();
        }

        private void Reset()
        {
            _configDic.Clear();
            _keyType = CsvValue.CsvValueType.None;
            _fieldNames.Clear();
        }

        public void ReadFile(string path)
        {
            using ( FileStream fs = new FileStream(path,FileMode.Open))
            {
                using ( StreamReader sr = new StreamReader(fs))
                {
                    Read(sr);
                    sr.Close();
                }
                fs.Close();
            }
        }

        public void Read(TextReader reader)
        {
            Reset();
            try
            {
                CsvReader csvReader = new CsvReader(reader, false);
                int fieldCount = csvReader.FieldCount;

                bool hasKey = false;
                int keyIndex = -1;
                Dictionary<int, CsvValue.CsvValueType> typeDic = new Dictionary<int, CsvValue.CsvValueType>();

                bool hasName = false;
                Dictionary<int, string> nameDic = new Dictionary<int, string>();
                foreach (string[] strings in csvReader)
                {
                    //check fieldCount
                    if (strings.Length != fieldCount)
                    {
                        StringBuilder sb = new StringBuilder();
                        bool first = true;
                        foreach (string s in strings)
                        {
                            if (first)
                            {
                                sb.Append(s);
                                first = false;
                            }
                            else
                            {
                                sb.Append(",").Append(s);
                            }
                        }
                        throw new CsvException(string.Format("fields length error,{0}", sb.ToString()));
                    }

                    //set key
                    if (!hasKey)
                    {
                        for (int i = 0; i < fieldCount; i++)
                        {
                            string low = strings[i].ToLower();
                            if (_typeDic.ContainsKey(low))
                            {
                                typeDic.Add(i, _typeDic[low]);
                            }
                            else if (_keyTypeDic.ContainsKey(low))
                            {
                                if (hasKey)
                                {
                                    throw new CsvException(string.Format("already has key{0}", low));
                                }
                                hasKey = true;
                                keyIndex = i;
                                typeDic.Add(i, _keyTypeDic[low]);
                                _keyType = _keyTypeDic[low];
                            }
                            else
                            {
                                throw new CsvException(string.Format("error header:{0}", strings[i]));
                            }

                        }
                        hasKey = true;
                        continue;
                    }

                    //set names
                    if (!hasName)
                    {
                        for (int i = 0; i < fieldCount; i++)
                        {
                            string name = strings[i];
                            if (_fieldNames.Contains(name))
                            {
                                throw new CsvException(string.Format("same field name:{0}", name));
                            }
                            nameDic.Add(i, name);
                            _fieldNames.Add(name);
                        }
                        hasName = true;
                        continue;
                    }

                    CsvConfig config = new CsvConfig();
                    for (int i = 0; i < fieldCount; i++)
                    {
                        string value = strings[i];
                        CsvConfigField field = new CsvConfigField(i, i == keyIndex, nameDic[i],
                            new CsvValue(typeDic[i], value));
                        config.AddField(field);
                    }

                    CsvValue keyValue = new CsvValue(typeDic[keyIndex], strings[keyIndex]);
                    foreach (KeyValuePair<CsvValue, CsvConfig> pair in _configDic)
                    {
                        if (pair.Key.Equals(keyValue))
                        {
                            throw new CsvException(string.Format("already contains key :{0}", keyValue.Value));
                        }
                    }
                    _configDic.Add(keyValue, config);
                    
                }
                if (!hasKey)
                {
                    throw new CsvException("miss key in header.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw new CsvException(e.Message);
            }
        }

        public IEnumerator<CsvConfig> GetEnumerator()
        {
            return _configDic.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _configDic.Values.GetEnumerator();
        }
    }
}
