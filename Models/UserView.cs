using System.ComponentModel.DataAnnotations;

namespace SMAP.Models
{
	public class UserView
	{
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

      
    }
}
