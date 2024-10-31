using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class BebeModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IbBebe { get; set; }
        public string? DescripcionBebe { get; set; }

        public List<BebeModel> ListaBebe()
        {
            List<BebeModel> ListaB = new List<BebeModel>();
            Conexion.Open();
            string Query = "SELECT iBbebe, descripcionBebe FROM bebe;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using(var objBebe = cmd.ExecuteReader())
            {
                while (objBebe.Read())
                {
                    ListaB.Add(new BebeModel
                    {
                        IbBebe = Convert.ToInt32(objBebe["iBbebe"]),
                        DescripcionBebe = objBebe["descripcionBebe"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaB;
        }
    }
}
