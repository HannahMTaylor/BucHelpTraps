using System.Collections.Generic;

namespace BucHelp.DatabaseServices
{
    public interface ITable
    {
        public Column[] Header { get; }
    }
}