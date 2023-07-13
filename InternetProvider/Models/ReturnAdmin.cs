using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class ReturnAdmin
    {
        [Required(ErrorMessage = "Admin Id is Required")]
        public string Admin_Id { get; set; }

        [Required(ErrorMessage = "Password is Required")]
        public string DecryptedPassword { get; set; }
    }
}
