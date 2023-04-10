using BucHelp.DatabaseServices;

namespace BucHelp.Data
{
    /// <summary>
    /// User class 
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string? UserName { get; set; } //Will be the email
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Affiliation { get; set; } //Student or Faculty
        public Profile? UserProfile { get; set; }
        public List<User> UsersList = new List<User>(); //Temp list for users until DB connections are created

        /// <summary>
        /// Set the user email and will also generate the user's display name
        /// </summary>
        /// <param name="email"></param>
        public void setUserName(string email)
        {
            Email = email;
            //Set the username here
            string username = email.Substring(0, email.IndexOf("@")); //Removed the @xxx.xxx
            UserName = username;
        }
        /// <summary>
        /// (Brandon) The only way I figured out on how to generate a Profile with the current User in a Razor component
        /// </summary>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="affiliation"></param>
        /// <param name="userProfile"></param>
        public User(string password, string email, string affiliation, Profile userProfile)
        {
            Password = password;
            setUserName(email);
            Affiliation = affiliation;
            UserProfile = userProfile;
            UserProfile.Username = UserName;
            UserProfile.Email = Email;
        }

        /// <summary>
        /// Constructor for User
        /// </summary>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="affiliation"></param>
        public User(int id, string password, string email, string affiliation)
        {
            UserId = id;
            Password = password;
            setUserName(email);
            Affiliation = affiliation;
            UserName = email;
        }

        /// <summary>
        /// Constructor for User
        /// </summary>
        /// <param name="password"></param>
        /// <param name="email"></param>
        /// <param name="affiliation"></param>
        /// /// <param name="userid"></param>
        public User(string password, string email, string affiliation, int userid)
        {
            Password = password;
            setUserName(email);
            Affiliation = affiliation;
            UserName = email;
            UserId = userid;
        }

        /// <summary>
        /// Base constructor for User
        /// </summary>
        public User()
        {

        }
    }
}
