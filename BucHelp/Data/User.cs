namespace BucHelp.Data
{
    public class User
    {
        public string UserName { get; set; } //Will be the email
        public string Password { get; set; }
        public string Email { get; set; }
        public string Affiliation { get; set; } //Student or Faculty

        public void setEmail(string email)
        {
            Email = email;
            //Set the username here as well
            string username = email.Substring(0, email.IndexOf("@")); //Removed the @xxx.xxx
            UserName = username;
        }

        public User(string password, string email, string affiliation)
        {
            Password = password;
            setEmail(email);
            Affiliation = affiliation;
        }

        public User()
        {

        }
    }
}
