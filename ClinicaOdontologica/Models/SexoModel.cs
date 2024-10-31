using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class SexoModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdSexo { get; set; }
        public string? Sexo { get; set; }

        public List<SexoModel> MostrarSexo()
        {
            List<SexoModel> ListaSexo = new List<SexoModel> { };
            Conexion.Open();
            String Query = "SELECT idSexo, sexo FROM sexo;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objSexo = cmd.ExecuteReader())
            {
                while (objSexo.Read())
                {
                    ListaSexo.Add(new SexoModel
                    {
                        IdSexo = Convert.ToInt32(objSexo["idSexo"]),
                        Sexo = objSexo["sexo"].ToString()
                    });
                }
            }                
            Conexion.Close();
            return ListaSexo; 
        }
    }
}
