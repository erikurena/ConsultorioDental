using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class HemorragiaDental
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdHemorragiaDental { get; set; }       
        public string? Hemorragia { get; set; }
        public string? TipoHemorragia { get; set; }

        public List<HemorragiaDental> MostrarHemorragia()
        {
            List<HemorragiaDental> ListaHemorragia = new List<HemorragiaDental>();
            Conexion.Open();
            string Query = "SELECT idHemorragiaDental, hemorragia FROM hemorragiadental WHERE idHemorragiaDental != 2;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    ListaHemorragia.Add(new HemorragiaDental
                    {
                        IdHemorragiaDental = Convert.ToInt32(reader["idHemorragiaDental"]),
                        Hemorragia = reader["hemorragia"].ToString(),
                    });
                }
            }
            Conexion.Close();
            return ListaHemorragia;
        }
    }
}
