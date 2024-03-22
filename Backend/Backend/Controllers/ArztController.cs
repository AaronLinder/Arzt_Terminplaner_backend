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
                        Arzt_ID = dr.GetInt32("Arzt_ID"),
                        Vorname = dr.GetString("Vorname"),
                        Nachname = dr.GetString("Nachname"),
                        Adresse = dr.GetString("Adresse"),
                        Ort = dr.GetString("Ort"),
                        Oefnugszeit = dr.GetDateTime("Oefnungszeit"),
                        Schlieﬂzeit = dr.GetDateTime("Schlieﬂzeit"),
                    }); ;
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
            aerzteListe
        );
        }

        [HttpGet(Name = "GetAerzt")]
        public Arzt GetArzt(int id)
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
                        Oefnugszeit = dr.GetDateTime("Oefnungszeit"),
                        Schlieﬂzeit = dr.GetDateTime("Schlieﬂzeit"),
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

        [HttpPost(Name = "AddArzt")]
        public void AddArzt(string Vorname, string Nachname, string Adresse, string Ort, DateTime Oeffnungszeit, DateTime Schlieﬂzeit)
        {
            MySqlCommand cmd = new MySqlCommand("INSERT INTO Arzt (Arzt_ID, Vorname, Nachname, Adresse, Ort, Oeffnungszeit, Schlieﬂzeit) VALUES ('" + Vorname + "', '" + Nachname + "', '" + Adresse + "', '" + Ort + "', '" + Oeffnungszeit + "', '" + Schlieﬂzeit + "')");
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