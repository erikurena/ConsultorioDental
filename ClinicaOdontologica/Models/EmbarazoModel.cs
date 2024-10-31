using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class EmbarazoModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdEmbarazo { get; set; }
        public string? Embarazo { get; set; }

        public List<EmbarazoModel> MostrarEmbarazo()
        {
            List<EmbarazoModel> ListaEmbarazo = new List<EmbarazoModel>();
            Conexion.Open();
            string Query = "SELECT idEmbarazo, descripcionEmbarazo FROM embarazo;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objEmbarazo = cmd.ExecuteReader())
            {
                while (objEmbarazo.Read())
                {
                    ListaEmbarazo.Add(new EmbarazoModel {
                        IdEmbarazo = Convert.ToInt32(objEmbarazo["idEmbarazo"]),
                        Embarazo = objEmbarazo["descripcionEmbarazo"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaEmbarazo;
        }
    }
}
