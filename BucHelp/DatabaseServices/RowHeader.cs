using System.Text;

namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// An object containing a read-only array of columns.
    /// </summary>
    public class RowHeader
    {
        private readonly Column[] columns;

        /// <summary>
        /// Create a new header.
        /// </summary>
        /// <param name="columns">array to copy</param>
        public RowHeader(params Column[] columns)
        {
            this.columns = (Column[]) columns.Clone();
        }

        /// <summary>
        /// Get the number of columns in this header.
        /// </summary>
        public int Length
        {
            get { return columns.Length; }
        }

        /// <summary>
        /// Access a column.
        /// </summary>
        /// <param name="i">index</param>
        /// <returns>
        ///     column at that index
        /// </returns>
        public Column this[int i]
        {
            get { return columns[i]; }
        }

        /// <summary>
        /// Return true if this header contains the specified column.
        /// </summary>
        /// <param name="col">column info to test for</param>
        /// <returns>
        ///     true if a column like the argument is in this header
        /// </returns>
        public bool ContainsColumnType(Column col)
        {
            return columns.Contains(col);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (Column col in columns)
            {
                sb.Append(col.ToString());
                sb.Append(',');
            }
            if (sb[sb.Length - 1] == ',') sb.Length--;
            return sb.ToString();
        }
    }
}
