using Microsoft.AspNetCore.Components;

namespace BucHelp.Data
{
    /// <summary>
    /// Utility class for the Navigation Menu
    /// </summary>
    public class NavUtil
    {
        public int ID = 0;

        /// <summary>
        /// Will set the Id of the User and will notify a state change to the navmenu passing this 
        /// variable to.
        /// </summary>
        /// <param name="id"></param>
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
