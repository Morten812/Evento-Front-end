using Microsoft.AspNetCore.Identity;

namespace Evento_Front_end.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
