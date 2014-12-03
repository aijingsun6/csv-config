
namespace Com.Alking.CSV
{
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
}
