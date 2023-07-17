using InternetProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Npgsql;
using System.Data;

namespace InternetProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        // private readonly string connectionString = "Server=localhost;Database=internet_provider;Port=5432;User Id=postgres;Password=Sagar@113;";

        private readonly IConfiguration Configuration;
        public EmployeeController(IConfiguration _configuration)
        {
            Configuration = _configuration;
        }


        //================================================= Insert Employee ============================

        [HttpPost] 
        public IActionResult InsertEmployee(Employee employee)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("select create_employee(@p_emp_id,@p_first_name,@p_last_name,@p_email,@p_phone,@p_department,@p_position)", connection))
                    {

                        command.CommandType = CommandType.Text;
                     //   command.CommandText = "create_employee(@p_emp_id,@p_first_name,@p_last_name,@p_email,@p_phone,@p_department,@p_position,@p_status,@p_requested_date)";
                        command.Parameters.AddWithValue("@p_emp_id", employee.Emp_Id);
                        command.Parameters.AddWithValue("@p_first_name", employee.First_Name);
                        command.Parameters.AddWithValue("@p_last_name", employee.Last_Name);
                        command.Parameters.AddWithValue("@p_email", employee.Email);
                        command.Parameters.AddWithValue("@p_phone", employee.Phone);
                        command.Parameters.AddWithValue("@p_department", employee.Department);
                        command.Parameters.AddWithValue("@p_position", employee.Position);
                       
                    
                        command.ExecuteNonQuery();
                    }
                }

                return Ok("Registration Successful.");
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        //================================================= Get All Employee Data ============================

        [HttpGet]
        public IActionResult GetEmployees()
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * FROM get_employees()", connection))
                    {
                        using (NpgsqlDataReader reader = command.ExecuteReader())
                        {
                            List<AllDetails> employees = new List<AllDetails>();

                            while (reader.Read())
                            {
                                AllDetails emp = new AllDetails
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Emp_Id = reader["emp_id"].ToString(),
                                    First_Name = reader["first_name"].ToString(),
                                    Last_Name = reader["last_name"].ToString(),
                                    Email = reader["email"].ToString(),
                                    Phone = (long)(reader["phone"] != DBNull.Value ? Convert.ToInt64(reader["phone"]) : (long?)null),
                                    Department = reader["department"].ToString(),
                                    Position = reader["p_position"].ToString(),
                                    Status = reader["status"].ToString(),
                                    Requested_Date = reader["requested_date"] != DBNull.Value ? Convert.ToDateTime(reader["requested_date"]) : (DateTime?)null,
                                    Approval_Date = reader["approval_date"] != DBNull.Value ? Convert.ToDateTime(reader["approval_date"]) : (DateTime?)null,
                                    Remark = reader["remark"].ToString()
                                };


                                employees.Add(emp);
                            }

                            return Ok(employees);
                        }
                    }
                }
            }
           
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
           
        }

  //================================================= Update Status ============================
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, [FromBody] Update_Employee updatedEmployee)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT update_employee_status(@p_id, @p_status, @p_remark);", connection))
                    {
                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@p_id", id);
                        command.Parameters.AddWithValue("@p_status", updatedEmployee.Status);
                        command.Parameters.AddWithValue("@p_remark", updatedEmployee.Remark);


                       // int RowsAffected = command.ExecuteNonQuery();
                       // Console.WriteLine(RowsAffected);
                        object result = command.ExecuteScalar();

                        if (result != null)
                        {
                            return Ok($"Employee with ID {id} has been updated successfully.");  
                        }
                        else
                        {
                            return NotFound($"Employee with ID {id} not found.");

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }




        [HttpGet("{emp_id}")]

        public IActionResult GetEmployee(string emp_id)

        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try

            {

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))

                {

                    connection.Open();



                    using (NpgsqlCommand command = new NpgsqlCommand("SELECT * from get_employee_id(@p_emp_id)", connection))

                    {

                        command.CommandType = CommandType.Text;

                        command.Parameters.AddWithValue("@p_emp_id", emp_id);



                        using (NpgsqlDataReader reader = command.ExecuteReader())

                        {

                            if (reader.Read())

                            {

                                SingleEmployee employee = new SingleEmployee

                                {
                                    Emp_Id = reader["employee_id"].ToString(),
                                    First_Name = reader["p_first_name"].ToString(),
                                    Last_Name = reader["p_last_name"].ToString(),
                                    Status = reader["p_status"].ToString(),
                                    Requested_Date = Convert.ToDateTime(reader["p_requested_date"]),
                                    Approval_Date = Convert.ToDateTime(reader["p_approval_date"]),
                                    Remark = reader["p_remark"].ToString()
                            };



                                return Ok(employee);

                            }

                            else

                            {

                                return NotFound($"Employee with ID {emp_id} not found.");

                            }

                        }

                    }

                }

            }

            catch (Exception ex)

            {

                return StatusCode(500, $"An error occurred: {ex.Message}");

            }

        }

    }
}
