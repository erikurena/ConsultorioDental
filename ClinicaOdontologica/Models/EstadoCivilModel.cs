using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class EstadoCivilModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdEstadoCivil { get; set; }
        public string? DescripcionEstCivil { get; set; }

        public List<EstadoCivilModel> MostrarEstadoCivil()
        {
            List<EstadoCivilModel> EstadoCivil = new List<EstadoCivilModel>();
            Conexion.Open();
            string Query = "SELECT idEstadoCivil, descripcionEstCivil FROM estadocivil;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            var objEstadoCivil = cmd.ExecuteReader();
            
                while (objEstadoCivil.Read())
                {
                    EstadoCivil.Add(new EstadoCivilModel
                    {
                        IdEstadoCivil = Convert.ToInt32(objEstadoCivil["idEstadoCivil"]),
                        DescripcionEstCivil = objEstadoCivil["descripcionEstCivil"].ToString()
                    });
                }
                   
            Conexion.Close();
            return EstadoCivil;          
        }
    }
}
