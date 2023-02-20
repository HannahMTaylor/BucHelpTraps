namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// Represents information about a database column, with name and SQLite type affinity. Immutable. Type affinities are ignored in CSV, but still enforced.
    /// </summary>
    public class Column
    {
        /// <summary>
        /// A column type.
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The column accepts strings.
            /// </summary>
            Text,
            /// <summary>
            /// The column accepts any numeric type.
            /// </summary>
            Numeric,
            /// <summary>
            /// The column accepts only integers.
            /// </summary>
            Integer,
            /// <summary>
            /// The column accepts only floating-point values.
            /// </summary>
            Real,
            /// <summary>
            /// The column accepts binary data.
            /// </summary>
            Blob
        }

        public static Type? TypeFromString(string str)
        {
            switch (str.ToUpperInvariant())
            {
                case "TEXT": return Type.Text;
                case "NUMERIC": return Type.Numeric;
                case "INTEGER": return Type.Integer;
                case "REAL": return Type.Real;
                case "BLOB": return Type.Blob;
                default: return null;
            }
        }

        public static string TypeToString(Type type)
        {
            switch (type)
            {
                case Type.Text: return "TEXT";
                case Type.Numeric: return "NUMERIC";
                case Type.Integer: return "INTEGER";
                case Type.Real: return "REAL";
                case Type.Blob: return "BLOB";
            }
            throw new Exception("unreachable");
        }

        /// <summary>
        /// Get the name of this column.
        /// </summary>
        public string Name { get; }
        /// <summary>
        /// Get the column type.
        /// </summary>
        public Column.Type ValueType { get; }
        /// <summary>
        /// Create a new column handle.
        /// </summary>
        /// <param name="name">the name of the column</param>
        /// <param name="type">the type of the column</param>
        /// <exception cref="ArgumentNullException">if the name is null</exception>
        /// <exception cref="ArgumentException">if the name length is zero characters</exception>
        public Column(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name is null");
            if (name.Length == 0) throw new ArgumentException("name length is zero");
            Name = name;
            ValueType = type;
        }

        /// <summary>
        /// Test if two columns have identical properties.
        /// </summary>
        /// <param name="obj">the other object</param>
        /// <returns>
        ///     true if the two columns have equal properties.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Column other = (Column) obj;
            return other.Name == Name && other.ValueType == ValueType;
        }
        
        /// <summary>
        /// Compute a hash code for this column info.
        /// </summary>
        /// <returns>
        ///     integer hash code
        /// </returns>
        public override int GetHashCode()
        {
            return 31 * Name.GetHashCode() ^ ValueType.GetHashCode();
        }

        public override string ToString()
        {
            return Name + " " + TypeToString(ValueType);
        }
    }
}
