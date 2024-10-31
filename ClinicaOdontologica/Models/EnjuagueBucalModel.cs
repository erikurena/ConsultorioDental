using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class EnjuagueBucalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdEnjuagueBucal { get; set; }
        public string? UsaEnjuagueBucal { get; set; }        

        public List<EnjuagueBucalModel> ListaEnjuageBucal()
        {
            List<EnjuagueBucalModel> ListaE = new List<EnjuagueBucalModel>();
            Conexion.Open();
            string Query = "SELECT idEnjuagueBucal, usaEnjuagueBucal FROM enjuaguebucal;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objEnjuague = cmd.ExecuteReader())
            {
                while (objEnjuague.Read())
                {
                    ListaE.Add(new EnjuagueBucalModel
                    {
                        IdEnjuagueBucal = Convert.ToInt32(objEnjuague["idEnjuagueBucal"]),
                        UsaEnjuagueBucal = objEnjuague["usaEnjuagueBucal"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaE;
        }
    }
}
