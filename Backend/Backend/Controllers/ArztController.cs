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

        [HttpGet("/GetAllAerzte")]
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
                        Arzt_ID = dr.GetInt32("Arzt_ID"),
                        Vorname = dr.GetString("Vorname"),
                        Nachname = dr.GetString("Nachname"),
                        Adresse = dr.GetString("Adresse"),
                        Ort = dr.GetString("Ort"),
                        Oeffnugszeit = dr.GetTimeSpan("Oeffnungszeit"),
                        Schlie�zeit = dr.GetTimeSpan("Schlie�zeit"),
                    });
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
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

        [HttpGet("/GetAerztById")]
        public Arzt GetArztByID(int id)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM Arzt where Arzt_ID=" + id);
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            Arzt arzt = new Arzt();
            try
            {
                MySqlDataReader dr;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    arzt = new Arzt
                    {
                        Arzt_ID = dr.GetInt32("Arzt_ID"),
                        Vorname = dr.GetString("Vorname"),
                        Nachname = dr.GetString("Nachname"),
                        Adresse = dr.GetString("Adresse"),
                        Ort = dr.GetString("Ort"),
                        Oeffnugszeit = dr.GetTimeSpan("Oeffnungszeit"),
                        Schlie�zeit = dr.GetTimeSpan("Schlie�zeit"),
                    };
                }
                dr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Kaputt");
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return (
            arzt
        );
        }



        [HttpPost("/AddArzt")]
        public IActionResult AddArzt(string Vorname, string Nachname, string Adresse, TimeSpan Oeffnungszeit, TimeSpan Schlie�zeit)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Arzt (Vorname, Nachname, Adresse, Oeffnungszeit, Schlie�zeit) VALUES ('" + Vorname + "', '" + Nachname + "', '" + Adresse + "', '" + Oeffnungszeit + "', '" + Schlie�zeit + "')");
            cmd.CommandType = CommandType.Text;
            cmd.Connection = connection;
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(400);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
            return StatusCode(200);
        }
    }
}