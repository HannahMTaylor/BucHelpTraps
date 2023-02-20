namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// A database driver.
    /// </summary>
    public interface IDatabaseDriver : IDisposable
    {
        /// <summary>
        /// Get a handle to a database table. Returns null if the table does not exist.
        /// </summary>
        /// <param name="name">table to get</param>
        /// <returns>
        ///     a handle to the requested table, or null if not found
        /// </returns>
        public ITable GetTableForName(string name);

        /// <summary>
        /// Create a table with the specified name and header. Fails with null if the table exists already.
        /// </summary>
        /// <param name="name">table name</param>
        /// <param name="header">table header</param>
        /// <returns>
        ///     a handle to the specified table
        /// </returns>
        public ITable CreateTable(string name, RowHeader header);

        /// <summary>
        /// Commit any unsaved changes to the database, if the implementation does not do so automatically.
        /// </summary>
        public void Commit();
    }
}
