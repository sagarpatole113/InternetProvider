using System.ComponentModel.DataAnnotations;

namespace InternetProvider.Models
{
    public class AllDetails
    {
 
        public int Id { get; set; }

        public string Emp_Id { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }

        public string Email { get; set; }

         public long Phone { get; set; }

        public string Department { get; set; }

        public string Position { get; set; }

        public string Status { get; set; }

        public DateTime?Requested_Date { get; set; }  
        public DateTime?Approval_Date { get; set; }

        public string Remark { get; set; }

      
    }
}
