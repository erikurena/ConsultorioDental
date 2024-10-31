using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class RespiradorOralModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdRespiradorOral { get; set; }
        public string? TipoRespirador { get; set; }

        public List<RespiradorOralModel> ListaRespiradorOral()
        {
            List<RespiradorOralModel> ListaRespirador = new List<RespiradorOralModel>();
            Conexion.Open();
            string Query = "SELECT idRespiradorOral, tipoRespirador FROM respiradororal;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objRespirador = cmd.ExecuteReader())
            {
                while (objRespirador.Read())
                {
                    ListaRespirador.Add(new RespiradorOralModel
                    {
                        IdRespiradorOral = Convert.ToInt32(objRespirador["idRespiradorOral"]),
                        TipoRespirador = objRespirador["tipoRespirador"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaRespirador;
        }
    }
}
