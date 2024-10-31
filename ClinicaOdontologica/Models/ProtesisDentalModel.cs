using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class ProtesisDentalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdProtesisDental { get; set; }
        public string? ProtesisDental { get; set; }

        public List<ProtesisDentalModel> ListaProtesisDental()
        {
            List<ProtesisDentalModel> ListaProtesis = new List<ProtesisDentalModel>();
            Conexion.Open();
            string Query = "SELECT IdProtesisDental, ProtesisDental FROM protesisdental;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            using (var objListaProtesis = cmd.ExecuteReader())
            {
                while (objListaProtesis.Read())
                {
                    ListaProtesis.Add(new ProtesisDentalModel
                    {
                        IdProtesisDental = Convert.ToInt32(objListaProtesis["IdProtesisDental"]),
                        ProtesisDental = objListaProtesis["ProtesisDental"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaProtesis;
        }
        

    }
}
