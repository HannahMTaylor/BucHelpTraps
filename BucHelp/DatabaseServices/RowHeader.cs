namespace BucHelp.DatabaseServices
{
    public class RowHeader
    {
        private readonly Column[] columns;

        public RowHeader(params Column[] columns)
        {
            this.columns = (Column[]) columns.Clone();
        }

        public int Length
        {
            get { return columns.Length; }
        }

        public Column this[int i]
        {
            get { return columns[i]; }
        }
    }
}
