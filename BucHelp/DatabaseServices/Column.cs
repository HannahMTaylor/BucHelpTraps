namespace BucHelp.DatabaseServices
{
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
    }
}
