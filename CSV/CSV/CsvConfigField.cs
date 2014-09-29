using System;

namespace Com.Alking.CSV
{
    public class CsvConfigField
    {
        public int Index { get; private set; }

        /// <summary>
        /// 是否是关键字
        /// </summary>
        public bool IsKey { get; private set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name {get; private set; }
        
        public CsvValue Value { get; private set; }

        public CsvConfigField(int index, bool isKey, string name, CsvValue value)
        {
            Index = index;
            IsKey = isKey;
            Name = name;
            Value = value;
        }

    }
}
