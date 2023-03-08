namespace BucHelp.Data
{
    /// <summary>
    /// Profile attached to a user
    /// </summary>
    public class Profile
    {
        public string? Name { get; set; } //The name the user wants to display, preferably their real name.
        public string? Major { get; set; } //May become an enum with a dropdown list later
        public string? Description { get; set; } //The profile Description
        //We should probably have more categories.

        /// <summary>
        /// This constructor is required because I (Brandon) cannot figure out how to run pre-declared code on a razor page with no caveats
        /// </summary>
        /// <param name="name"></param>
        /// <param name="major"></param>
        /// <param name="description"></param>
        public Profile (string name, string major, string description)
        {
            Name = name;
            Major = major;
            Description = description;
        }
        /// <summary>
        /// The default profile, where the name is the same as the username.
        /// </summary>
        /// <param name="name"></param>
        public Profile (string name)
        {
            Name = name;
        }
        /// <summary>
        /// Base constructor for Profile
        /// </summary>
        public Profile()
        {

        }
    }
}
