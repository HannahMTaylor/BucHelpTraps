using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace BucHelp.DatabaseServices
{
    // All driver code specific to CSVs is in this file.

    /// <summary>
    /// CSV files-based driver, one CSV file per table. Explicit commit is required.
    /// </summary>
    public class CSVDriver : IDatabaseDriver
    {
        private readonly string folderpath;
        private readonly Dictionary<string, RowHeader> headers;
        private readonly Dictionary<string, List<Row>> tables;
        private readonly Dictionary<string, CSVTableHandle> tableHandles;

        public CSVDriver(string folderpath)
        {
            if (folderpath == null) throw new ArgumentNullException("folderpath is null");
            if (!Directory.Exists(folderpath)) throw new ArgumentException("directory at " + folderpath + " does not exist");
            this.folderpath = folderpath;
            headers = new Dictionary<string, RowHeader>();
            tables = new Dictionary<string, List<Row>>();
            tableHandles = new Dictionary<string,CSVTableHandle>();
            // parse all CSVs in folder
            foreach (string path in Directory.EnumerateFiles(folderpath))
            {
                if (!path.EndsWith(".csv")) continue;
                LoadCSV(path);
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public ITable GetTableForName(string name)
        {
            RowHeader rowHeader = headers[name];
            List<Row> rows = tables[name];
            if (rows == null || rowHeader == null) return null;
            return tableHandles.GetValueOrDefault(name, new CSVTableHandle(rowHeader, rows));
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void LoadCSV(string path)
        {
            StreamReader sr = new StreamReader(path);
            // Determine table name
            string tablename = Path.GetFileNameWithoutExtension(path);
            List<Row> tablerows = new List<Row>();

            // Header reading process
            // First line is column names and determining row width
            List<string> names = new List<string>();
            int stop; // reason why ReadElement stopped, -1 if nothing read so far

            // Read column names while elements can be read off the current line
            stop = -1;
            while (stop < 1) // only stop at line or EOF stops
            {
                string elem = ReadElement(sr, out stop);
                if (elem.Length == 0)
                {
                    throw new Exception("zero length column name encountered");
                }
                names.Add(elem);
            }
            // Second line is column types
            List<string> types = new List<string>();
            // Read column types
            stop = -1;
            while (stop < 1) // only stop at line or EOF stops
            {
                string elem = ReadElement(sr, out stop);
                if (elem.Length == 0)
                {
                    throw new Exception("zero length type name encountered");
                }
                if (!(elem == "NUMERIC" || elem == "INTEGER" || elem == "REAL" || elem == "TEXT" || elem == "BLOB"))
                {
                    throw new Exception("type " + elem + " is not a valid type (expecting NUMERIC, INTEGER, REAL, TEXT, or BLOB)");
                }
                types.Add(elem);
            }
            // Check if columns have proper length, including hint on unexpected EOF
            if (types.Count != names.Count)
            {
                string errorMessage = $"Mismatch between counts: names ({names.Count}), types ({types.Count}).";
                if (stop == EOF_STOP)
                {
                    errorMessage += " (Unexpected end of file is likely cause)";
                }
                throw new Exception(errorMessage);
            }
            // Make RowHeader
            Column[] columns = new Column[names.Count];
            for (int i = 0; i < columns.Length; i++)
            {
                Column.Type? type = Column.TypeFromString(types[i]);
                // this should not be null at this point
                columns[i] = new Column(names[i], type.Value);
            }
            RowHeader header = new RowHeader(columns);
            Console.WriteLine(header.ToString());
            // Third line on is data values
            // While not EOF... (this also considers EOF immediately after the header)
            // read a row
            while (stop != EOF_STOP)
            {
                // Make row object
                Row row = new Row(header);
                // Reset the stop
                stop = -1;
                List<string> values = new List<string>();
                while (stop < 1) // stop at line or EOF
                {
                   values.Add(ReadElement(sr, out stop));
                }
                // Parse values according to the row header
                // Check for mismatched lengths
                if (values.Count != header.Length)
                {
                    throw new Exception($"row length mismatch: values ({values.Count}), expected ({header.Length})");
                }
                // Parse text values for each column
                for (int i = 0; i < columns.Length; i++)
                {
                    Column column = columns[i]; // expected column type and name
                    Console.WriteLine("Have: " + values[i] + " Col " + column.ToString());
                    switch (column.ValueType)
                    {
                        case Column.Type.Numeric:
                            {
                                // Try parsing as long, then double, or fail
                                long longvalue;
                                double doublevalue;
                                if (long.TryParse(values[i], out longvalue))
                                {
                                    row.SetAsLong(column.Name, longvalue);
                                }
                                else if (double.TryParse(values[i], out doublevalue))
                                {
                                    row.SetAsDouble(column.Name, doublevalue);
                                }
                                else
                                {
                                    throw new Exception("Cannot parse " + values[i] + " as NUMERIC");
                                }
                            }
                            break;
                        case Column.Type.Integer:
                            {
                                // Try parsing as long, or fail
                                long longvalue;
                                if (long.TryParse(values[i], out longvalue))
                                {
                                    row.SetAsLong(column.Name, longvalue);
                                }
                                else
                                {
                                    throw new Exception("Cannot parse " + values[i] + " as INTEGER");
                                }
                            }
                            break;
                        case Column.Type.Real:
                            {
                                // Try parsing as double, or fail
                                double doublevalue;
                                if (double.TryParse(values[i], out doublevalue))
                                {
                                    row.SetAsDouble(column.Name, doublevalue);
                                }
                                else
                                {
                                    throw new Exception("Cannot parse " + values[i] + " as REAL");
                                }
                            }
                            break;
                        case Column.Type.Text:
                            row.SetAsString(column.Name, values[i]);
                            break;
                        case Column.Type.Blob:
                            // Base64 code
                            throw new NotImplementedException();
                            //break;
                    }
                }
                // Install the row in the table
                Console.WriteLine(row.ToString());
                tablerows.Add(row);
            }
            // close stream
            sr.Close();
            // Install table
            tables[tablename] = tablerows;
        }

        // how ReadElement terminates
        private const int COMMA_STOP = 0;
        private const int LINE_STOP = 1;
        private const int EOF_STOP = 2;
        private string ReadElement(StreamReader sr, out int stop)
        {
            StringBuilder sb = new StringBuilder();
            bool escaping = false; // has escape slash been read in last iteration
            int ch;
            // while not at stream EOF, read
            while ((ch = sr.Read()) != -1)
            {
                if (escaping)
                {
                    // Escape flag was set so add character raw and clear flag
                    sb.Append((char) ch);
                    escaping = false;
                }
                else if (ch == '\\')
                {
                    // Escape flag not set, but backslash seen
                    // Set escape flag and do nothing
                    escaping = true;
                }
                else if (ch == ',')
                {
                    // Comma stop
                    stop = COMMA_STOP;
                    return sb.ToString();
                }
                else if (ch == '\r' || ch == '\n')
                {
                    // CRLF check
                    // if '\r' and peeked '\n', drop it on the floor
                    if (ch == '\r' && sr.Peek() == '\n') sr.Read();
                    // Line stop
                    stop = LINE_STOP;
                    return sb.ToString();
                }
                else
                {
                    // Normal character
                    sb.Append((char) ch);
                }
            }
            stop = EOF_STOP;
            return sb.ToString();
        }

        private string EscapeString(string input)
        {
            StringBuilder output = new StringBuilder();
            foreach (char c in input)
            {
                // if the character is problematic, escape it with \
                if (c == '\\' || c == ',' || c == '\r' || c == '\n')
                {
                    output.Append('\\').Append(c);
                }
                else
                {
                    // normal character
                    output.Append(c);
                }
            }
            return output.ToString();
        }
    }

    class CSVTableHandle : ITable
    {
        private readonly RowHeader rowHeader;
        private readonly List<Row> rows;

        public CSVTableHandle(RowHeader rowHeader, List<Row> rows) 
        {
            this.rowHeader = rowHeader;
            this.rows = rows;
        }
        public RowHeader Header { get { return rowHeader; } }

        public void Delete(Predicate<Row> where)
        {
            rows.RemoveAll(where);
        }

        public void Insert(params Row[] inrows)
        {
            foreach (Row inrow in inrows)
            {
                if (!rows.Contains(inrow))
                {
                    rows.Add(new Row(inrow));
                }
            }
        }

        public IEnumerable<Row> Select(Predicate<Row> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Row> SelectAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Predicate<Row> where, string key, object value)
        {
            throw new NotImplementedException();
        }

        public void UpdateMultiple(Predicate<Row> where, IDictionary<string, object> keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
