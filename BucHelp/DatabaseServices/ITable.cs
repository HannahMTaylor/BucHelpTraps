using System.Collections.Generic;

namespace BucHelp.DatabaseServices
{
    /// <summary>
    /// A handle to a database table.
    /// </summary>
    public interface ITable
    {
        /// <summary>
        /// Get information about the table's columns.
        /// </summary>
        public RowHeader Header { get; }

        /// <summary>
        /// Returns an object that can be enumerated to produce all rows in the database.
        /// </summary>
        /// <returns>
        ///     an Enumerable traversing all rows
        /// </returns>
        public IEnumerable<Row> SelectAll();

        /// <summary>
        /// Returns an object that can be enumerated to produce all rows where the predicate returns true.
        /// </summary>
        /// <param name="where">the predicate for which to test all rows</param>
        /// <returns>
        ///     an Enumerable traversing all matching rows
        /// </returns>
        public IEnumerable<Row> Select(Predicate<Row> where);

        /// <summary>
        /// Insert the provided rows into the database.
        /// </summary>
        /// <param name="row">rows to insert</param>
        public void Insert(params Row[] row);

        /// <summary>
        /// Set the "key" column's value to "value" in all rows where the predicate returns true.
        /// </summary>
        /// <param name="where">predicate to match</param>
        /// <param name="key">column to modify</param>
        /// <param name="value">value to set on matching rows</param>
        public void Update(Predicate<Row> where, string key, object value);

        /// <summary>
        /// Update multiple column values in all rows where the predicate returns true.
        /// </summary>
        /// <param name="where">predicate to match</param>
        /// <param name="keyValues">dictionary of columns to affect, along with their new values</param>
        public void UpdateMultiple(Predicate<Row> where, IDictionary<string, object> keyValues);

        /// <summary>
        /// Delete rows where the predicate matches.
        /// </summary>
        /// <param name="where">Predicate to match.</param>
        public void Delete(Predicate<Row> where);
    }
}