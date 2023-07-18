using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class Employee
    {

        [Required(ErrorMessage = "Required")]
        [StringLength(8, MinimumLength = 8, ErrorMessage = "Enter Valid 8 letter Employee Id")]
        public string Emp_Id { get; set; }


        [Required (ErrorMessage = "Employee First Name is required")]
        public string First_Name { get; set; }
        
        [Required (ErrorMessage = "Employee Last Name is required")]
        public string Last_Name { get; set; }
        
        [Required (ErrorMessage = "Employee Email is required")]
        [EmailAddress (ErrorMessage = "Please Enter valid Email")]
        public string Email { get; set; }


        [Required  (ErrorMessage = "Phone Number is required")]
        public long Phone { get; set; }


        [Required (ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required (ErrorMessage = "Position is required")]
        public string Position { get; set; }

    }
}
