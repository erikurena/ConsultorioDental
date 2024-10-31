using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class RolModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdRol { get; set; }
        public string? TipoRol { get; set; }

        public List<RolModel> ListaRol()
        {
            List<RolModel> ListaR = new List<RolModel>();
            Conexion.Open();
            string Query = "SELECT idRol, tipoRol FROM rol;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objRol = cmd.ExecuteReader())
            {
                while (objRol.Read())
                {
                    ListaR.Add(new RolModel
                    {
                        IdRol = Convert.ToInt32(objRol["idRol"]),
                        TipoRol = objRol["tipoRol"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaR;
        }
    }
}
