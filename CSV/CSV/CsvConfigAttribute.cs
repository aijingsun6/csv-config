using System;

namespace Com.Alking.CSV
{
    [AttributeUsageAttribute(AttributeTargets.Property,AllowMultiple = true)]
    class CsvConfigAttribute : Attribute
    {
        public string Name { get; set; }

        public CsvValueType ValueType { get; set; }

    }
}
