using MySql.Data.MySqlClient;
namespace ClinicaOdontologica.Models
{
    public class HiloDentalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdHiloDental { get; set; }
        public string? UsaHiloDental { get; set; }

        public List<HiloDentalModel> ListaHiloDental()
        {
            List<HiloDentalModel> ListaH = new List<HiloDentalModel>();
            Conexion.Open();
            string Query = "SELECT idHiloDental, usahiloDental FROM hilodental;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objHiloDental = cmd.ExecuteReader())
            {
                while (objHiloDental.Read())
                {
                    ListaH.Add(new HiloDentalModel
                    {
                        IdHiloDental = Convert.ToInt32(objHiloDental["idHiloDental"]),
                        UsaHiloDental = objHiloDental["usahiloDental"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaH;
        }

    }
}
