// ---------------------------------------------------------------------------
// Creator’s name and email: Stephen Maurer Maurers@etsu.edu
// Creation Date: 2/14/2023
// Last Modified: 2/21/2023
// ---------------------------------------------------------------------------
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace BucHelp.Data
{
    /// <summary>
    /// Login page model for verification
    /// </summary>
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Credential Credential { get; set; }
        public void OnGet()
        {

        }
        
        public void OnPost() 
        {

        }
    }

    /// <summary>
    /// Credential class used for verification.
    /// </summary>
    public class Credential
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
