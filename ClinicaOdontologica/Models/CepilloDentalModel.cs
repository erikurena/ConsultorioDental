using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class CepilloDentalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdCepilloDental { get; set; }
        public string? UsaCepilloDental { get; set; }

        public List<CepilloDentalModel> ListaCepilloDental()
        {
            List<CepilloDentalModel> ListaC = new List<CepilloDentalModel> ();
            Conexion.Open();
            string Query = "SELECT idCepilloDental, usaCepilloDental FROM cepillodental;";
            MySqlCommand cmd = new MySqlCommand (Query, Conexion);
            using(var objCepilloDental = cmd.ExecuteReader())
            {
                while(objCepilloDental.Read())
                {
                    ListaC.Add(new CepilloDentalModel
                    {
                        IdCepilloDental = Convert.ToInt32(objCepilloDental["idCepilloDental"]),
                        UsaCepilloDental = objCepilloDental["usaCepilloDental"].ToString()
                    }) ;
                }
            }
            Conexion.Close();
            return ListaC;
        }
    }
}
