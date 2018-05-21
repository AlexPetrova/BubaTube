using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace BubaTube.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class User : IdentityUser
    {
        public ICollection<User> FavouriteUsers { get; set; }
    }
}
