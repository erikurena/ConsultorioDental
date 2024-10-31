using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class LugarNacimientoModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdLugarNacimiento { get; set; }
        public string? LugarNacimiento { get; set; }

        public List<LugarNacimientoModel> MostrarLugarNacimiento()
        {
            List<LugarNacimientoModel> ListaLugarNacimiento = new List<LugarNacimientoModel> ();
            Conexion.Open();
            string Query = "SELECT idLugarNacimiento, departamento FROM lugarnacimiento";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            using (var objLugarNacimiento = cmd.ExecuteReader())
            {
                while (objLugarNacimiento.Read())
                {
                    ListaLugarNacimiento.Add(new LugarNacimientoModel
                    {
                        IdLugarNacimiento = Convert.ToInt32(objLugarNacimiento["idLugarNacimiento"]),
                        LugarNacimiento = objLugarNacimiento["departamento"].ToString()
                    });
                }               
            }
            Conexion.Close();
            return ListaLugarNacimiento;
        }
    }
}
