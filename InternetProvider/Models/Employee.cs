using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required (ErrorMessage = "Employee Id is required")]
        [StringLength(6, ErrorMessage = "Employee Id must be 6 character")]
        public string Emp_Id { get; set; }


        [Required (ErrorMessage = "Employee First Name is required")]
        public string First_Name { get; set; }
        
        [Required (ErrorMessage = "Employee Last Name is required")]
        public string Last_Name { get; set; }
        
        [Required (ErrorMessage = "Employee Email is required")]
        [EmailAddress (ErrorMessage = "Please Enter valid Email")]
        public string Email { get; set; }


        [Required (ErrorMessage = "Phone Number is required")]
        [StringLength(10,ErrorMessage = "Enter 10 digits only")]
        public string Phone { get; set; }


        [Required (ErrorMessage = "Department is required")]
        public string Department { get; set; }

        [Required (ErrorMessage = "Position is required")]
        public string Position { get; set; }

    }
}
