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
        private readonly Dictionary<string, List<Row>> tables;

        public CSVDriver(string folderpath)
        {
            if (folderpath == null) throw new ArgumentNullException("folderpath is null");
            if (!Directory.Exists(folderpath)) throw new ArgumentException("directory at " + folderpath + " does not exist");
            this.folderpath = folderpath;
            tables = new Dictionary<string, List<Row>>();
            // parse all CSVs in folder
            foreach (string path in Directory.EnumerateFiles(folderpath))
            {
                LoadCSV(path);
            }
        }

        public void Commit()
        {
            throw new NotImplementedException();
        }

        public ITable GetTableForName(string name)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        private void LoadCSV(string path)
        {
            StreamReader sr = new StreamReader(path);
            // Header reading process
            // First line is column names
            // Second line is column types
            // Third line on is data values
            sr.Close();
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
                    output.Append(c);
                }
            }
            return output.ToString();
        }
    }
}
