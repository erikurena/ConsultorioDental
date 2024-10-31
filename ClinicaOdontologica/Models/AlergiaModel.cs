using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class AlergiaModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdAlergia { get; set; }
        public string? Alergia { get; set; }

        public List<AlergiaModel> MostrarAlergia()
        {
            List<AlergiaModel> ListaAlergia = new List<AlergiaModel>();
            Conexion.Open();
            string Query = "SELECT idAlergia, descripcionAlergia FROM alergia;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objAlergia = cmd.ExecuteReader())
            {
                while (objAlergia.Read())
                {
                    ListaAlergia.Add(new AlergiaModel{
                        IdAlergia = Convert.ToInt32(objAlergia["idAlergia"]),
                        Alergia = objAlergia["descripcionAlergia"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaAlergia;
        }


    }
}
