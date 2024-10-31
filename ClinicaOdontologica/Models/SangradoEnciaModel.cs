using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class SangradoEnciaModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdSangradoEncia { get; set; }
        public string? TieneSangradoEncia { get; set; }
        

        public List<SangradoEnciaModel> ListaSangradoEncia()
        {
            List<SangradoEnciaModel> ListaS = new List<SangradoEnciaModel>();
            Conexion.Open();
            string Query = "SELECT idSangradoEncia, tieneSangradoEncia   FROM sangradoencia;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objSangradoEncia = cmd.ExecuteReader())
            {
                while (objSangradoEncia.Read())
                {
                    ListaS.Add(new SangradoEnciaModel
                    {
                        IdSangradoEncia = Convert.ToInt32(objSangradoEncia["idSangradoEncia"]),
                        TieneSangradoEncia = objSangradoEncia["tieneSangradoEncia"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaS;
        }
    }
}
