using System.Numerics;
using System.Xml.Linq;

namespace BucHelp.Data
{
    /// <summary>
    /// Profile attached to a user
    /// </summary>
    public class Profile
    {
        public string? Username { get; set; }//The user name of the account. Required to display
        public string? Name { get; set; } //The name the user wants to display, preferably their real name. Optional, section is empty when not filled
        public string? Major { get; set; } //May become an enum with a dropdown list later. Optional, section is empty when not filled
        public string? Email { get; set; } //The users email, defaults to their own email. Optional, section is empty when not filled
        public string? Description { get; set; } //The profile Description. Optional, section is empty when not filled
        public string? Phone { get; set; } //The users phone number. Optional, section is invisible when not filled.
        public string? SocialMedia { get; set; } //One social media the user has. Optional, section is invisible when not filled.
        public int? Points { get; set; } //Points the user can accumulate. Always displays as 0 on user initiate.
        public int? Stars { get; set; } //These do not publicly display. Instead, they are an amount of stars awarded by other users. Starts at 0, shows as 2.5 when empty.
        public int? Reviews { get; set; } //The number of reviews the user has had by other users. This will divide stars to get the average number of stars they have. Internal only


        //****
        //****
        //****
        //We need a way to store images! Currently we do not and all images are hard coded links instead.
        //****
        //****
        //****

        /// <summary>
        /// This constructor is the full constructor for a pre-existing user.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="name"></param>
        /// <param name="major"></param>
        /// <param name="email"></param>
        /// <param name="description"></param>
        /// <param name="phone"></param>
        /// <param name="socialmedia"></param>
        /// <param name="points"></param>
        /// <param name="stars"></param>
        /// <param name="reviews"></param>
        public Profile (string username, string name, string major, string email, string description, string phone, string socialmedia, int points, int stars, int reviews)
        {
            Username = username;
            Name = name;
            Major = major;
            Email = email;
            Description = description;
            Phone = phone;
            SocialMedia = socialmedia;
            Points = points;
            Stars = stars;
            Reviews = reviews;
        }
        /// <summary>
        /// Constructor for a mostly filled profile where the remaining details are created by the User class. This one contains the numerical values
        /// </summary>
        /// <param name="name"></param>
        /// <param name="major"></param>
        /// <param name="description"></param>
        /// <param name="phone"></param>
        /// <param name="socialmedia"></param>
        /// <param name="points"></param>
        /// <param name="stars"></param>
        /// <param name="reviews"></param>
        public Profile(string name, string major, string description, string phone, string socialmedia, int points, int stars, int reviews)
        {
            Username = "Blank";
            Name = name;
            Major = major;
            Email = "Blank";
            Description = description;
            Phone = phone;
            SocialMedia = socialmedia;
            Points = points;
            Stars = stars;
            Reviews = reviews;
        }
        /// <summary>
        /// Constructor for a mostly filled profile where the remaining details are created by the User class
        /// </summary>
        /// <param name="name"></param>
        /// <param name="major"></param>
        /// <param name="description"></param>
        /// <param name="phone"></param>
        /// <param name="socialmedia"></param>
        public Profile(string name, string major, string description, string phone, string socialmedia)
        {
            Username = "Blank";
            Name = name;
            Major = major;
            Email = "Blank";
            Description = description;
            Phone = phone;
            SocialMedia = socialmedia;
            Points = 0;
            Stars = 0;
            Reviews = 0;
        }

        /// <summary>
        /// Base constructor for Profile
        /// </summary>
        public Profile()
        {
            Username = "Blank";
            Name = "";
            Major = "";
            Email = "Blank";
            Description = "";
            Phone = "";
            SocialMedia = "";
            Points = 0;
            Stars = 0;
            Reviews = 0;
        }
    }
}
