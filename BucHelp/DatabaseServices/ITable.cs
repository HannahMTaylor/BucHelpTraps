using System.Collections.Generic;

namespace BucHelp.DatabaseServices
{
    public interface ITable
    {
        public RowHeader Header { get; }

        public IEnumerable<Row> SelectAll();
        public IEnumerable<Row> Select(Predicate<Row> where);
        public void Insert(params Row[] row);

        public void Update(Predicate<Row> where, string key, object value);
        public void UpdateMultiple(Predicate<Row> where, IDictionary<string,object> keyValues);

        public void Delete(Predicate<Row> where);
    }
}