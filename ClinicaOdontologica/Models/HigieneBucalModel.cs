using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class HigieneBucalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdHigieneBucal { get; set; }
        public string? DescripcionHigieneBucal { get; set; }        

        public List<HigieneBucalModel> ListaHigieneBucal()
        {
            List<HigieneBucalModel> ListaHB = new List<HigieneBucalModel>();
            Conexion.Open();
            string Query = "SELECT idHigieneBucal, descripcionHigieneBucal FROM higienebucal;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objHigieneBucal = cmd.ExecuteReader())
            {
                while (objHigieneBucal.Read())
                {
                    ListaHB.Add(new HigieneBucalModel
                    {
                        IdHigieneBucal = Convert.ToInt32(objHigieneBucal["idHigieneBucal"]),
                        DescripcionHigieneBucal = objHigieneBucal["descripcionHigieneBucal"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaHB;
        }
    }
}
