using System;
using System.Text;

namespace Com.Alking.CSV
{
    
    public sealed class CsvValue
    {
        /// <summary>
        ///a tiny floating point value
        /// <value>0.0001</value>
        /// </summary>
        public const float Epsilon = 0.0001f;

        /// <summary>
        /// separate char between array
        /// <example>
        /// 1;2;3;4;5
        /// ';' is seperator char
        /// </example>
        /// </summary>
        public const char DefArraySeparateChar = ';';

        private static char _arraySeparateChar = DefArraySeparateChar;
        /// <summary>
        /// get and set array separate char
        /// </summary>
        /// <exception cref="CsvException">value is ',' when set</exception>
        public static char ArraySeparateChar
        {
            get { return _arraySeparateChar; }
            set
            {
                if (value == ',')
                {
                    throw new CsvException("separate char can not be ','");
                }
                _arraySeparateChar = value;
            }
        }

        /// <summary>
        /// <para>value type</para>
        /// <list type="bullet">
        /// <item><see cref="Int"/></item>
        /// <item><see cref="Long"/></item>
        /// <item><see cref="Bool"/></item>
        /// <item><see cref="Float"/></item>
        /// <item><see cref="Double"/></item>
        /// <item><see cref="String"/></item>
        /// </list>
        /// </summary>
        public enum CsvValueType
        {
            /// <summary>
            /// none
            /// </summary>
            None,
            
            /// <summary>
            /// type of <see cref="bool"/>
            /// </summary>
            Bool,
            /// <summary>
            /// type of <see cref="bool"/> array
            /// </summary>
            ArrayBool,
            
            /// <summary>
            /// type of <see cref="int"/>
            /// </summary>
            Int,
            /// <summary>
            /// type of <see cref="int"/> array
            /// </summary>
            ArrayInt,

            /// <summary>
            /// type of <see cref="long"/>
            /// </summary>
            Long,
            /// <summary>
            /// type of <see cref="long"/> array
            /// </summary>
            ArrayLong,
            
            /// <summary>
            /// type of <see cref="float"/>
            /// </summary>
            Float,
            /// <summary>
            /// type of <see cref="float"/> array
            /// </summary>
            ArrayFloat,

            /// <summary>
            /// type of <see cref="double"/>
            /// </summary>
            Double,
            /// <summary>
            /// type of <see cref="double"/> array
            /// </summary>
            ArrayDouble,
            
            /// <summary>
            /// type of <see cref="string"/>
            /// </summary>
            String,
            /// <summary>
            /// type of <see cref="string"/> array
            /// </summary>
            ArrayString
        }

        /// <summary>
        /// type of this value
        /// </summary>
        public CsvValueType Type { get; private set; }

        /// <summary>
        /// it can be :
        /// <list type="bullet">
        /// <item><see cref="int"/></item>
        /// <item><see cref="int"/>[]</item>
        /// <item><see cref="long"/></item>
        /// <item><see cref="long"/>[]</item>
        /// <item><see cref="bool"/></item>
        /// <item><see cref="bool"/>[]</item>
        /// <item><see cref="float"/></item>
        /// <item><see cref="float"/>[]</item>
        /// <item><see cref="double"/></item>
        /// <item><see cref="double"/>[]</item>
        /// <item><see cref="string"/></item>
        /// <item><see cref="string"/>[]</item>
        /// </list>
        /// </summary>
        public object Value { get; private set; }

        private CsvValue()
        {
            Value = null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="strValue"></param>
        /// <exception cref="CsvException">
        /// strValue can not change to CsvValue
        /// 
        /// </exception>
        public CsvValue(CsvValueType type,string strValue)
        {
            Type = type;
            switch (type)
            {
                case CsvValueType.Int:
                    int intValue = default(int);
                    if (string.IsNullOrEmpty(strValue) || int.TryParse(strValue, out intValue))
                    {
                        Value = intValue;
                    }
                    else
                    {
                        throw new CsvException(string.Format("parse int error,string is {0}",strValue));
                    }
                    break;

                case CsvValueType.ArrayInt:
                    string[] intStrings = strValue.Split(new char[] {ArraySeparateChar},StringSplitOptions.RemoveEmptyEntries);
                    int[] intArray = new int[intStrings.Length];
                    for (int i = 0;i < intStrings.Length;i++)
                    {
                        int intValue2 = default(int);
                        if (string.IsNullOrEmpty(intStrings[i]) || int.TryParse(intStrings[i],out intValue2))
                        {
                            intArray[i] = intValue2;
                        }
                        else
                        {
                            throw new CsvException(string.Format("parse int array error,string is {0}", strValue));
                        }
                    }
                    Value = intArray;
                    break;

                case CsvValueType.Long:
                    long longValue = default(long);
                    if (string.IsNullOrEmpty(strValue) || long.TryParse(strValue, out longValue))
                    {
                        Value = longValue;
                    }
                    else
                    {
                        throw new CsvException(string.Format("parse long error,string is {0}", strValue));
                    }
                    break;

                case CsvValueType.ArrayLong:
                    string[] longStrings = strValue.Split(new char[] {ArraySeparateChar},StringSplitOptions.RemoveEmptyEntries);
                    long[] longArray = new long[longStrings.Length];
                    for (int i = 0; i < longStrings.Length; i++)
                    {
                        long longValue2 = default(long);
                        if (string.IsNullOrEmpty(longStrings[i]) || long.TryParse(longStrings[i], out longValue2))
                        {
                            longArray[i] = longValue2;
                        }
                        else
                        {
                            throw new CsvException(string.Format("parse long array error,string is {0}", strValue));
                        }
                    }
                    Value = longArray;
                    break;

                case CsvValueType.Bool:
                    string low = null == strValue ? null : strValue.ToLower();
                    if (low == "true" || low == "yes")
                    {
                        Value = true;
                    }
                    else if (string.IsNullOrEmpty(low) || low == "false" || low == "no")
                    {
                        Value = false;
                    }
                    else
                    {
                        throw new CsvException(string.Format("parse bool error,string is {0}", strValue));
                    }
                    break;

                case CsvValueType.ArrayBool:
                    string[] boolStrings = strValue.Split(new char[] { ArraySeparateChar }, StringSplitOptions.RemoveEmptyEntries);
                    bool[] boolArray = new bool[boolStrings.Length];
                    for (int i = 0; i < boolStrings.Length; i++)
                    {
                        if (boolStrings[i].ToLower() == "true" || boolStrings[i].ToLower() == "yes")
                        {
                            boolArray[i] = true;
                        }
                        else if (boolStrings[i].ToLower() == "false" || boolStrings[i].ToLower() == "no")
                        {
                            boolArray[i] = false;
                        }
                        else 
                        {
                            throw new CsvException(string.Format("parse bool array error,string is {0}", strValue));
                        }
                    }
                    Value = boolArray;
                    break;
                case CsvValueType.Float:
                    float fValue = default(float);
                    if (string.IsNullOrEmpty(strValue) || float.TryParse(strValue, out fValue))
                    {
                        Value = fValue;
                    }
                    else
                    {
                        throw new CsvException(string.Format("parse float error,string is {0}", strValue));
                    }
                    break;

                    
                case CsvValueType.ArrayFloat:
                    string[] floatStrings = strValue.Split(new char[] { ArraySeparateChar }, StringSplitOptions.RemoveEmptyEntries);
                    float[] floatArray = new float[floatStrings.Length];
                    for (int i = 0; i < floatStrings.Length; i++)
                    {
                        float fValue2 = default(float);
                        if (string.IsNullOrEmpty(floatStrings[i]) || float.TryParse(floatStrings[i],out fValue2))
                        {
                            floatArray[i] = fValue2;
                        }
                        else
                        {
                            throw new CsvException(string.Format("parse float array error,string is {0}", strValue));
                        }
                    }
                    Value = floatArray;
                    break;

                case CsvValueType.Double:
                    double dValue = default(double);
                    if (string.IsNullOrEmpty(strValue) || double.TryParse(strValue, out dValue))
                    {
                        Value = dValue;
                    }
                    else
                    {
                        throw new CsvException(string.Format("parse double error,string is {0}", strValue));
                    }
                    break;
                case CsvValueType.ArrayDouble:
                    string[] doubleStrings = strValue.Split(new char[] { ArraySeparateChar }, StringSplitOptions.RemoveEmptyEntries);
                    double[] doubleArray = new double[doubleStrings.Length];
                    for (int i = 0; i < doubleStrings.Length; i++)
                    {
                        double dValue2 = default(double);
                        if (string.IsNullOrEmpty(doubleStrings[i]) || double.TryParse(doubleStrings[i], out dValue2))
                        {
                            doubleArray[i] = dValue2;
                        }
                        else
                        {
                            throw new CsvException(string.Format("parse double array error,string is {0}", strValue));
                        }
                    }
                    Value = doubleArray;
                    break;
                case CsvValueType.String:
                    Value = strValue ?? string.Empty;
                    break;
                case CsvValueType.ArrayString:
                    string[] stringArray = strValue.Split(new char[] { ArraySeparateChar }, StringSplitOptions.RemoveEmptyEntries);
                    Value = stringArray;
                    break;
                case CsvValueType.None:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("type");
            }
        }

        #region implicit bool
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not a bool</exception>
        public static implicit operator bool(CsvValue value)
        {
            if (value.Type == CsvValueType.Bool)
            {
                return (bool) value.Value;
            }
            throw new CsvException(string.Format("this is not a bool,but is {0}",value.Value));
        }

        public static implicit operator CsvValue(bool b)
        {
            return new CsvValue{Type = CsvValueType.Bool,Value = b};
        }
        #endregion implicit bool

        #region implicit bool array
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not bool array</exception>
        /// <param name="v"></param>
        /// <returns>
        /// return null when parameter v is null
        /// </returns>
        public static implicit operator bool[](CsvValue v)
        {
            if (v == null)
            {
                return null;
            }
            if (v.Type == CsvValueType.ArrayBool)
            {
                bool[] array = v.Value as bool[];
                return array;
            }
            throw new CsvException(string.Format("this is not bool array,but {0}", v.Type));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <exception cref="ArgumentException"> when paramter array is null</exception>
        /// <returns></returns>
        public static implicit operator CsvValue(bool[] array)
        {
            if (array == null)
            {
                throw new ArgumentException("parameter array is null");
            }
            CsvValue v = new CsvValue();
            v.Type = CsvValueType.ArrayBool;
            v.Value = array;
            return v;
        }
        #endregion implicit bool array

        #region implicit int
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not int value</exception>
        public static implicit operator int(CsvValue value)
        {
            if (value.Type == CsvValueType.Int)
            {
                return (int)value.Value;
            }
            if (value.Type == CsvValueType.Long)
            {
                long longValue = (long) value.Value;
                if (longValue > int.MaxValue || longValue < int.MinValue)
                {
                    throw new CsvException(string.Format("can not change to int,{0}",value.Value));
                }
                return (int) longValue;
            }
            if (value.Type == CsvValueType.Float)
            {
                float f = (float) value.Value;
                if (f > int.MaxValue || f < int.MinValue)
                {
                    throw new CsvException(string.Format("can not change to int,{0}", value.Value));
                }
                return (int) f;
            }
            if (value.Type == CsvValueType.Double)
            {
                double f = (double)value.Value;
                if (f > int.MaxValue || f < int.MinValue)
                {
                    throw new CsvException(string.Format("can not change to int,{0}", value.Value));
                }
                return (int)f;
            }
            throw new CsvException(string.Format("this is not a int,but is {0}", value.Value));
        }



        public static implicit operator CsvValue(int value)
        {
            return new CsvValue{Type = CsvValueType.Int,Value = value};
        }

        #endregion implicit int

        #region implicit int array
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not int array</exception>
        /// <param name="v"></param>
        /// <returns>
        /// return null when v is null
        /// </returns>
        public static implicit operator int[](CsvValue v)
        {
            if (v == null)
            {
                return null;
            }
            if (v.Type == CsvValueType.ArrayInt)
            {
                int[] array = v.Value as int[];
                return array;
            }
            throw new CsvException(string.Format("this is not int array,but {0}", v.Type));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <returns>
        /// null when array is null
        /// </returns>
        public static implicit operator CsvValue(int[] array)
        {
            if (array == null)
            {
                return null;
            }
            CsvValue value = new CsvValue();
            value.Type = CsvValueType.ArrayInt;
            value.Value = array;
            return value;
        }
        #endregion implicit int array

        #region implicit long
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not long value</exception>
        public static implicit operator long(CsvValue value)
        {
            if (value.Type == CsvValueType.Int)
            {
                return (int) value.Value;
            }
            if (value.Type == CsvValueType.Long)
            {
                return (long)value.Value;
            }
            if (value.Type == CsvValueType.Float)
            {
                float v = (float) value.Value;
                return (long) v;
            }
            if (value.Type == CsvValueType.Double)
            {
                double v = (double) value.Value;
                return (long) v;

            }
            throw new CsvException(string.Format("this is not a long,but is {0}", value.Value));
        }

        public static implicit operator CsvValue(long value)
        {
            return new CsvValue{Type = CsvValueType.Long,Value = value};
        }
        #endregion

        #region implicit long array

        public static implicit operator long[](CsvValue v)
        {
            if (v == null)
            {
                return null;
            }
            if (v.Type == CsvValueType.ArrayLong)
            {
                long[] array = v.Value as long[];
                return array;
            }
            throw new CsvException(string.Format("this is not long array,but {0}",v.Type));
        }

        public static implicit operator CsvValue(long[] array)
        {
            if (array == null)
            {
                return null;
            }
            CsvValue value = new CsvValue();
            value.Type = CsvValueType.ArrayLong;
            value.Value = array;
            return value;
        }
        #endregion implicit long array

        #region implicit float
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not float value</exception>
        public static implicit operator float(CsvValue value)
        {

            if (value.Type == CsvValueType.Int)
            {
                return (int)value.Value;
            }
            if (value.Type == CsvValueType.Long)
            {
                return (long)value.Value;
            }
            if (value.Type == CsvValueType.Float)
            {
                float v = (float)value.Value;
                return v;
            }
            if (value.Type == CsvValueType.Double)
            {
                double v = (double)value.Value;
                return (float)v;

            }
            throw new CsvException(string.Format("this is not a float,but is {0}", value.Value));
        }

        public static implicit operator CsvValue(float value)
        {
            return new CsvValue{Type = CsvValueType.Float,Value = value};
        }
        #endregion

        #region implicit float array

        public static implicit operator float[](CsvValue value)
        {
            if (value == null)
            {
                return null;
            }
            if (value.Type == CsvValueType.ArrayFloat)
            {
                float[] array = value.Value as float[];
                return array;
            }
            throw new CsvException(string.Format("this is not float array ,but {0}",value.Type));
        }

        public static implicit operator CsvValue(float[] array)
        {
            if (array == null)
            {
                return null;
            }
            CsvValue value = new CsvValue();
            value.Type = CsvValueType.ArrayFloat;
            value.Value = array;
            return value;
        }
        #endregion implicit float array

        #region implicit double
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not double value</exception>
        public static implicit operator double(CsvValue value)
        {
            if (value.Type == CsvValueType.Int)
            {
                return (int)value.Value;
            }
            if (value.Type == CsvValueType.Long)
            {
                return (long)value.Value;
            }
            if (value.Type == CsvValueType.Float)
            {
                float v = (float)value.Value;
                return v;
            }
            if (value.Type == CsvValueType.Double)
            {
                double v = (double)value.Value;
                return v;

            }
            throw new CsvException(string.Format("this is not a double,but is {0}", value.Value));
        }

        public static implicit operator CsvValue(double value)
        {
            return new CsvValue{Type = CsvValueType.Double,Value = value};
        }
        #endregion

        #region implicit double array

        public static implicit operator double[](CsvValue value)
        {
            if (value == null)
            {
                return null;
            }
            if (value.Type == CsvValueType.ArrayDouble)
            {
                double[] array = value.Value as double[];
                return array;
            }
            throw new CsvException(string.Format("this is not double array,but {0}",value.Type));
        }

        public static implicit operator CsvValue(double[] array)
        {
            if (array == null)
            {
                return null;
            }
            CsvValue value = new CsvValue();
            value.Type = CsvValueType.ArrayDouble;
            value.Value = array;
            return value;
        }
        #endregion implicit double array

        #region implicit string
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="CsvException">this is not string value</exception>
        public static implicit operator string(CsvValue value)
        {
            if (value.Type == CsvValueType.String)
            {
                return (string)value.Value;
            }
            throw new CsvException(string.Format("this is not a string,but is {0}", value.Value));
        }

        public static implicit operator CsvValue(string value)
        {
            return new CsvValue{Type = CsvValueType.String,Value = value};
        }
        #endregion

        #region implicit string array
        public static implicit operator string[](CsvValue value)
        {
            if (value == null)
            {
                return null;
            }
            if (value.Type == CsvValueType.ArrayString)
            {
                string[] array = value.Value as string[];
                return array;
            }
            throw new CsvException(string.Format("this is not double array,but {0}", value.Type));
        }

        public static implicit operator CsvValue(string[] array)
        {
            if (array == null)
            {
                return null;
            }
            CsvValue value = new CsvValue();
            value.Type = CsvValueType.ArrayString;
            value.Value = array;
            return value;
        }
        #endregion implicit string array 
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object other)
        {
            if (other == null)
            {
                return false;
            }
            CsvValue otherValue = other as CsvValue;
            if (otherValue == null || otherValue.Type == CsvValueType.None)
            {
                return false;
            }
            switch (Type)
            {
                case CsvValueType.Bool:
                    if (otherValue.Type != CsvValueType.Bool)
                    {
                        return false;
                    }
                    return (bool) Value == (bool) otherValue.Value;
                case CsvValueType.Int:
                    if (otherValue.Type == CsvValueType.Bool || otherValue.Type == CsvValueType.String)
                    {
                        return false;
                    }
                    int intValue = (int) Value;
                    switch (otherValue.Type)
                    {
                        case CsvValueType.Int:
                            return intValue == (int) otherValue.Value;
                        case CsvValueType.Long:
                            return intValue == (long)otherValue.Value;
                        case CsvValueType.Float:
                            return Approximately(intValue, (float) otherValue.Value);
                        case CsvValueType.Double:
                            return Approximately(intValue, (double) otherValue.Value);
                    }
                    return false;
                case CsvValueType.Long:
                    if (otherValue.Type == CsvValueType.Bool || otherValue.Type == CsvValueType.String)
                    {
                        return false;
                    }
                    long longValue = (long) Value;
                     
                     switch (otherValue.Type)
                     {
                        case CsvValueType.Int:
                             return longValue == (int) (otherValue.Value);
                        case CsvValueType.Long:
                             return longValue == (long) (otherValue.Value);
                        case CsvValueType.Float:
                             return Approximately(longValue, (float)otherValue.Value);
                        case CsvValueType.Double:
                             return Approximately(longValue, (double) otherValue.Value);
                     }
                    return false;
                case CsvValueType.Float:
                    if (otherValue.Type == CsvValueType.Bool || otherValue.Type == CsvValueType.String)
                    {
                        return false;
                    }
                    float fValue = (float) Value;
                     switch (otherValue.Type)
                     {
                        case CsvValueType.Int:
                             return Approximately((int) otherValue.Value, fValue);
                        case CsvValueType.Long:
                             return Approximately((long)otherValue.Value,fValue);
                        case CsvValueType.Float:
                             return Approximately((float)otherValue.Value,fValue);
                        case CsvValueType.Double:
                             return Approximately((double) otherValue.Value,fValue);
                     }
                    return false;

                case CsvValueType.Double:
                    if (otherValue.Type == CsvValueType.Bool || otherValue.Type == CsvValueType.String)
                    {
                        return false;
                    }
                    double dValue = (double) Value;
                     switch (otherValue.Type)
                     {
                        case CsvValueType.Int:
                             return Approximately((int) otherValue.Value, dValue);
                        case CsvValueType.Long:
                             return Approximately((long)otherValue.Value,dValue);
                        case CsvValueType.Float:
                             return Approximately((float)otherValue.Value,dValue);
                        case CsvValueType.Double:
                             return Approximately((double) otherValue.Value,dValue);
                     }
                    return false;
                case CsvValueType.String:
                    if (otherValue.Type != CsvValueType.String)
                    {
                        return false;
                    }
                    return Value.Equals(otherValue.Value);
                case CsvValueType.None:
                default:
                    return false;
            }

            return false;
        }

        public override string ToString()
        {
            string value = "";
            switch (Type)
            {
                case CsvValueType.Bool:
                case CsvValueType.Int:
                case CsvValueType.Long:
                case CsvValueType.Float:
                case CsvValueType.Double:
                case CsvValueType.String:
                    value = Value.ToString();
                    break;
                case CsvValueType.ArrayBool:
                    bool[] bools = Value as bool[];
                    value = Array2String<bool>(bools);
                    break;
                case CsvValueType.ArrayInt:
                    int[] ints = Value as int[];
                    value = Array2String<int>(ints);
                    break;
                case CsvValueType.ArrayLong:
                    long[] longs = Value as long[];
                    value = Array2String<long>(longs);
                    break;
                case CsvValueType.ArrayFloat:
                    float[] floats = Value as float[];
                    value = Array2String<float>(floats);
                    break;
                case CsvValueType.ArrayDouble:
                    double[] doubles = Value as double[];
                    value = Array2String<double>(doubles);
                    break;
                case CsvValueType.ArrayString:
                    string[] strings = Value as string[];
                    value = Array2String<string>(strings);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return string.Format("(type:{0},value:{1})",Type,value);
        }

        /// <summary>
        /// array to string
        /// </summary>
        /// <typeparam name="T">bool,int,long,float,double,string</typeparam>
        /// <param name="array"></param>
        /// <returns></returns>
        protected string Array2String<T>(T[] array)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[");
            if (array == null || array.Length == 0)
            {
                
            }
            else
            {
                bool first = true;
                foreach (T t in array)
                {
                    if (first)
                    {
                        first = false;
                        sb.Append(t);
                    }
                    else
                    {
                        sb.Append(",").Append(t);
                    }

                }
            }
            sb.Append("]");
            return sb.ToString();
        }

        private bool Approximately(int v1, float v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(int v1, double v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(long v1, float v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(long v1,double v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(float v1, float v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(float v1, double v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
        private bool Approximately(double v1, double v2)
        {
            double c = Math.Abs(v1 - v2);
            if (c < Epsilon)
            {
                return true;
            }
            return false;
        }
    }
}
