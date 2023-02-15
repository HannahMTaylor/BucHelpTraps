namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// Represents a database column, with name and SQLite type affinity. Immutable. Type affinities are ignored in CSV.
    /// </summary>
    public class Column
    {
        public enum Type
        {
            Text, Numeric, Integer, Real, Blob;
        }
        public string Name { get; }
        public Column.Type ValueType { get; }

        public Column(string name, Type type)
        {
            if (name == null) throw new ArgumentNullException("name is null");
            if (name.Length == 0) throw new ArgumentException("name length is zero");
            Name = name;
            ValueType = type;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            Column other = (Column) obj;
            return other.Name == Name && other.ValueType == ValueType;
        }
        
        public override int GetHashCode()
        {
            return 31 * Name.GetHashCode() ^ ValueType.GetHashCode();
        }
    }
}
