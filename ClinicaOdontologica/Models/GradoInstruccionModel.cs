using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class GradoInstruccionModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdGradoInstruccion { get; set; }
        public string? GradoInstruccion { get; set; }

        public List<GradoInstruccionModel> MostrarInstruccion()
        {
            List<GradoInstruccionModel> GradoInstruccion = new List<GradoInstruccionModel>();
            Conexion.Open();
            string Query = "select idGradoInstruccion, descripcionInstruccion FROM gradoInstruccion;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            var objGradoInstruccion = cmd.ExecuteReader();
            
                while (objGradoInstruccion.Read())
                {
                    GradoInstruccion.Add(new GradoInstruccionModel
                    {
                        IdGradoInstruccion = Convert.ToInt32(objGradoInstruccion["idGradoInstruccion"]),
                        GradoInstruccion = objGradoInstruccion["descripcionInstruccion"].ToString()
                    });
                
            }
            Conexion.Close();
            return GradoInstruccion;
        }
    }
}
