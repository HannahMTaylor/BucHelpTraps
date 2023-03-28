using Microsoft.AspNetCore.Components;

namespace BucHelp.Data
{
    public class NavUtil
    {
        public int ID = 0;

        public void SetID(int id)
        {
            if(!int.Equals(ID, id))
            {
                ID = id;
                NotifyStateChanged();
            }
        }

        public event Action OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
