namespace BucHelp.DatabaseServices
{
    public interface IDatabaseDriver : IDisposable
    {
        public ITable GetTableForName(string name);
        public void Commit();
    }
}
