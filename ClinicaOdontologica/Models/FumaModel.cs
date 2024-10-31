using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class FumaModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdFuma { get; set; }
        public string? DescipcionFuma { get; set; }

        public List<FumaModel> ListaFuma()
        {
            List<FumaModel> ListaF = new List<FumaModel>();
            Conexion.Open();
            string Query = "SELECT idFuma, descripcionFuma FROM fuma;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objFuma = cmd.ExecuteReader())
            {
                while (objFuma.Read())
                {
                    ListaF.Add(new FumaModel
                    {
                        IdFuma = Convert.ToInt32(objFuma["idFuma"]),
                        DescipcionFuma = objFuma["descripcionFuma"].ToString()
                    }) ;
                }
            }
            Conexion.Close();
            return ListaF;
        }
    }
}
