using InternetProvider.Models;
using Microsoft.AspNetCore.Mvc;
using Npgsql;
using System.Data;

namespace InternetProvider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration Configuration;
        public AdminController(IConfiguration _configuration) 
        {
              Configuration = _configuration;
        }

        [HttpPost("register")]
        public IActionResult InsertAdmin(Create_Admin adminDto)
        {
            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {

                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("select create_admin(@p_admin_id,@p_admin_password)", connection))
                    {

                        command.CommandType = CommandType.Text;
                        string username = adminDto.Admin_Id;
                        string password = adminDto.EncryptedPassword;
                       
                        command.Parameters.AddWithValue("@p_admin_id", username);
                        command.Parameters.AddWithValue("@p_admin_password", password);

                        object result = command.ExecuteScalar();

                        if(result != null)
                        {
                            return Ok("Admin record inserted successfully.");
                        }
                        else { return BadRequest("Something went wrong"); }
                    }
                }


                
            }
            catch (Exception ex)
            {
                // Handle exceptions and return appropriate response
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        //======================================= Authenticate ============================
        [HttpPost("login")]
        public IActionResult GetAdminDetails(ReturnAdmin returnAdmin)
        {

            string connectionString = Configuration.GetConnectionString("DefaultString");

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    using (NpgsqlCommand command = new NpgsqlCommand("select is_admin_valid(@p_admin_id,@p_admin_password)", connection))
                    {
                        command.CommandType = CommandType.Text;

                        var username = returnAdmin.Admin_Id;
                        var pass = returnAdmin.DecryptedPassword;

                        command.Parameters.AddWithValue("@p_admin_id", username);
                        command.Parameters.AddWithValue("@p_admin_password", pass);

                        bool result = (bool)command.ExecuteScalar();
                       
                        if (result == true)
                        {
                            return Ok("Admin login authentication successfully.");
                        }
                        else
                        {
                            return NotFound("Unuthorized login credintials");

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

    }
}
