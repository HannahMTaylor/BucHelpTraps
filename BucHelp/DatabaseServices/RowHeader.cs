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

        /// <summary>
        /// Return the column for the given name.
        /// </summary>
        /// <param name="name">the column name</param>
        /// <returns>
        ///     the column or null if not present
        /// </returns>
        public Column FindColumnForName(string name)
        {
            return columns.FirstOrDefault(col => col.Name == name, null);
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

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Column[] other = ((RowHeader)obj).columns;
            if (other.Length != columns.Length) return false;
            for (int i = 0; i < columns.Length; i++)
            {
                if (other[i] != columns[i]) return false;
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash = 0;
            for (int i = 0; i < columns.Length; i++)
            {
                hash = 31 * hash + columns[i].GetHashCode();
            }
            return hash;
        }
    }
}
