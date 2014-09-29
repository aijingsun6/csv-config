using System.Collections.Generic;
namespace Com.Alking.CSV
{
    /// <summary>
    /// 一条配置文件
    /// </summary>
    public class CsvConfig : IEnumerable<CsvConfigField>
    {
        private readonly Dictionary<string,CsvConfigField> _namedFields = new Dictionary<string, CsvConfigField>();

        private readonly Dictionary<int,CsvConfigField> _indexedFields = new Dictionary<int, CsvConfigField>(); 

        /// <summary>
        /// maybe return null
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public CsvConfigField this[int index]
        {
            get
            {
                if (_indexedFields.ContainsKey(index))
                {
                    return _indexedFields[index];
                }

                return null;
            }
            
        }

        /// <summary>
        /// maybe return null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public CsvConfigField this[string name]
        {
            get
            {
                if (_namedFields.ContainsKey(name))
                {
                    return _namedFields[name];
                }

                return null;
            }

        }

        public CsvConfig()
        {
            _namedFields.Clear();
            _indexedFields.Clear();
        }

        public bool AddField(CsvConfigField field)
        {
            if (_namedFields.ContainsKey(field.Name))
            {
                return false;
            }
            _namedFields.Add(field.Name,field);
            _indexedFields.Add(field.Index,field);
            return true;
        }

        public IEnumerator<CsvConfigField> GetEnumerator()
        {
            return _namedFields.Values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _namedFields.Values.GetEnumerator();
        }
    }
}
