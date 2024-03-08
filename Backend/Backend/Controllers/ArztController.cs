using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Data;

namespace Backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArztController : ControllerBase
    {

        private readonly ILogger<Arzt> _logger;
        private MySqlConnection connection;

        public ArztController(ILogger<Arzt> logger)
        {
            _logger = logger;

            string connectionString = String.Format("server={0};port={1};user id={2}; password={3}; database={4}", "10.110.49.67", 3306, "root", "mysecret", "Arzt_Terminplaner");

            connection = new MySqlConnection(connectionString);
            connection.Open();
        }

        [HttpGet(Name = "GetAllAerzte")]
        public List<Arzt> Get()
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Arzt");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            List<Arzt> aerzteListe = new List<Arzt>();
            string name = "Not found";
            try
            {
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    aerzteListe.Add(new Arzt
                    {
                        name = dr.GetString("name"),
                        adresse = dr.GetString("adresse")
                    });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("linder");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return (
            aerzteListe
        );
        }

        [HttpPost(Name = "AddArzt")]
        public void AddArzt(string name, string adresse)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Arzt (Name, Adresse) VALUES ('" + name + "', '" + adresse + "')");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine("linder");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}