using System;

namespace Com.Alking.CSV
{
    public class CsvException:Exception
    {
        public CsvException()
        {

        }

        public CsvException(string message) : base(message)
        {

        }
    }
}
