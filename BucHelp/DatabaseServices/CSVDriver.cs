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
            throw new NotImplementedException();
        }
    }
}
