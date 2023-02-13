using System.Collections.Generic;

namespace BucHelp.DatabaseServices
{
    public interface ITable
    {
        public System.Collections.Generic.IEnumerator<ElementType> GetEnumerator()
        {

            throw new NotImplementedException();
            yield return default(ElementType);
        }
    }
}