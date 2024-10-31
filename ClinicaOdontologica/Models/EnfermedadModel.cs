using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class EnfermedadModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdEnfermedad { get; set; }
        public string? Enfermedad{ get; set; }

        public List<EnfermedadModel> MostrarEnfermedad()
        {
            List<EnfermedadModel>? ListEnfermedad = new List<EnfermedadModel>();
            Conexion.Open();
            string Query = "SELECT idEnfermedad, tipoEnfermedad FROM enfermedad;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            using (var objEnfermedad = cmd.ExecuteReader())
            {
                while (objEnfermedad.Read())
                {
                    ListEnfermedad.Add(new EnfermedadModel
                    {
                        IdEnfermedad = Convert.ToInt32(objEnfermedad["idEnfermedad"]),
                        Enfermedad = objEnfermedad["tipoEnfermedad"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListEnfermedad;
        }
    }
}
