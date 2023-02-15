namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// A mutable row. Note that changes made here are not persisted. Use the methods on ITable
    /// to write the rows to the database.
    /// </summary>
    public class Row
    {
        private readonly RowHeader rowHeader;
        private readonly IDictionary<string, object> values;

        public RowHeader Header { get { return rowHeader; } }

        private void ThrowIfNotNumericInteger(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Numeric)) || 
                !rowHeader.ContainsColumnType(new Column(column, Column.Type.Integer)))
            {
                throw new ArgumentException("Column not present or not of INTEGER or NUMERIC type");
            }
        }

        private void ThrowIfNotNumericReal(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Numeric)) ||
                !rowHeader.ContainsColumnType(new Column(column, Column.Type.Integer)))
            {
                throw new ArgumentException("Column not present or not of INTEGER or REAL type");
            }
        }

        public Row(RowHeader rowHeader)
        {
            if (rowHeader == null) throw new ArgumentNullException(nameof(rowHeader));
            this.rowHeader = rowHeader;
            values = new Dictionary<string,object>();
        }

        public Row(RowHeader rowHeader, IDictionary<string, object> values)
        {
            if (rowHeader == null) throw new ArgumentNullException(nameof(rowHeader));
            if (values == null) throw new ArgumentNullException(nameof(values));
            this.rowHeader = rowHeader;
            this.values = values;
        }

        public int GetAsInt(string column)
        {
            ThrowIfNotNumericInteger(column);
            return (int) values[column];
        }

        public long GetAsLong(string column)
        {
            ThrowIfNotNumericInteger(column);
            return (long) values[column];
        }

        public float GetAsFloat(string column)
        {
            ThrowIfNotNumericReal(column);
            return (float) values[column];
        }

        public double GetAsDouble(string column)
        {
            ThrowIfNotNumericReal(column);
            return (double) values[column];
        }

        public string GetAsString(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Text)))
            {
                throw new ArgumentException("Column not present or not of TEXT type");
            }
            return (string) values[column];
        }

        public byte[] GetAsBlob(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Blob)))
            {
                throw new ArgumentException("Column not present or not of BLOB type");
            }
            return (byte[]) values[column];
        }

        public void SetAsInt(string column, int value)
        {
            ThrowIfNotNumericInteger(column);
            values[column] = value;
        }

        public void SetAsLong(string column, long value)
        {
            ThrowIfNotNumericInteger(column);
            values[column] = value;
        }

        public void SetAsFloat(string column, float value)
        {
            ThrowIfNotNumericReal(column);
            values[column] = value;
        }

        public void SetAsDouble(string column, double value)
        {
            ThrowIfNotNumericReal(column);
            values[column] = value;
        }

        public void SetAsString(string column, string value)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Text)))
            {
                throw new ArgumentException("Column not present or not of TEXT type");
            }
            values[column] = value;
        }

        public void SetAsBlob(string column, byte[] value)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Blob)))
            {
                throw new ArgumentException("Column not present or not of BLOB type");
            }
            values[column] = value;
        }

        internal IDictionary<string,object> GetKeyValues()
        {
            return values;
        }
    }
}
