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

        /// <summary>
        /// Return the row header from which this row derives.
        /// </summary>
        public RowHeader Header { get { return rowHeader; } }

        /// <summary>
        /// Construct a row constrained to the provided header.
        /// </summary>
        /// <param name="rowHeader">header to be based</param>
        /// <exception cref="ArgumentNullException">if the header is null</exception>
        public Row(RowHeader rowHeader)
        {
            if (rowHeader == null) throw new ArgumentNullException(nameof(rowHeader));
            this.rowHeader = rowHeader;
            values = new Dictionary<string, object>();
        }

        /// <summary>
        /// Throw if the column is not compatible with integers.
        /// </summary>
        /// <param name="column">target</param>
        /// <exception cref="ArgumentException"></exception>
        private void ThrowIfNotNumericInteger(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Numeric)) || 
                !rowHeader.ContainsColumnType(new Column(column, Column.Type.Integer)))
            {
                throw new ArgumentException("Column not present or not of INTEGER or NUMERIC type");
            }
        }

        /// <summary>
        /// Throw if the column is not compatible with floats.
        /// </summary>
        /// <param name="column">target</param>
        /// <exception cref="ArgumentException"></exception>
        private void ThrowIfNotNumericReal(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Numeric)) ||
                !rowHeader.ContainsColumnType(new Column(column, Column.Type.Integer)))
            {
                throw new ArgumentException("Column not present or not of INTEGER or REAL type");
            }
        }

        /// <summary>
        /// Get the specified column as an integer (NUMERIC, INTEGER)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as int
        /// </returns>
        public int GetAsInt(string column)
        {
            ThrowIfNotNumericInteger(column);
            return (int) values[column];
        }

        /// <summary>
        /// Get the specified column as a long integer (NUMERIC, INTEGER)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as long
        /// </returns>
        public long GetAsLong(string column)
        {
            ThrowIfNotNumericInteger(column);
            return (long) values[column];
        }

        /// <summary>
        /// Get the specified column as a float (NUMERIC, REAL)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as float
        /// </returns>
        public float GetAsFloat(string column)
        {
            ThrowIfNotNumericReal(column);
            return (float) values[column];
        }

        /// <summary>
        /// Get the specified column as a double-precision float (NUMERIC, REAL)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as double
        /// </returns>
        public double GetAsDouble(string column)
        {
            ThrowIfNotNumericReal(column);
            return (double) values[column];
        }

        /// <summary>
        /// Get the specified column as a string (TEXT)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as string
        /// </returns>
        /// <exception cref="ArgumentException">if column is not TEXT type</exception>
        public string GetAsString(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Text)))
            {
                throw new ArgumentException("Column not present or not of TEXT type");
            }
            return (string) values[column];
        }

        /// <summary>
        /// Get the specified column as binary data (BLOB)
        /// </summary>
        /// <param name="column">target</param>
        /// <returns>
        ///     value as byte array
        /// </returns>
        /// <exception cref="ArgumentException">if column is not BLOB type</exception>
        public byte[] GetAsBlob(string column)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Blob)))
            {
                throw new ArgumentException("Column not present or not of BLOB type");
            }
            return (byte[]) values[column];
        }

        /// <summary>
        /// Set target column to int value (NUMERIC, INTEGER)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        public void SetAsInt(string column, int value)
        {
            ThrowIfNotNumericInteger(column);
            values[column] = value;
        }

        /// <summary>
        /// Set target column to long value (NUMERIC, INTEGER)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        public void SetAsLong(string column, long value)
        {
            ThrowIfNotNumericInteger(column);
            values[column] = value;
        }

        /// <summary>
        /// Set target column to float value (NUMERIC, REAL)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        public void SetAsFloat(string column, float value)
        {
            ThrowIfNotNumericReal(column);
            values[column] = value;
        }

        /// <summary>
        /// Set target column to double value (NUMERIC, REAL)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        public void SetAsDouble(string column, double value)
        {
            ThrowIfNotNumericReal(column);
            values[column] = value;
        }

        /// <summary>
        /// Set target column to string value (TEXT)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        /// <exception cref="ArgumentException">if column is not TEXT type</exception>
        public void SetAsString(string column, string value)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Text)))
            {
                throw new ArgumentException("Column not present or not of TEXT type");
            }
            values[column] = value;
        }

        /// <summary>
        /// Set target column to binary data value (BLOB)
        /// </summary>
        /// <param name="column">target</param>
        /// <param name="value">value</param>
        /// <exception cref="ArgumentException">if column is not BLOB type</exception>
        public void SetAsBlob(string column, byte[] value)
        {
            if (!rowHeader.ContainsColumnType(new Column(column, Column.Type.Blob)))
            {
                throw new ArgumentException("Column not present or not of BLOB type");
            }
            values[column] = value;
        }

        /// <summary>
        /// Get the internal map for this row
        /// </summary>
        /// <returns>
        ///     internal dictionary
        /// </returns>
        internal IDictionary<string,object> GetKeyValues()
        {
            return values;
        }
    }
}
