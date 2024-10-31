using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class Conexion
    {
        private string CadenaConexion = string.Empty;
        public Conexion() {
            var Builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

             CadenaConexion = Builder.GetSection("ConnectionStrings:CadenaConexion").Value;
         }
        public string getCadenaConexion()
        {
            return CadenaConexion;
        }
    }
}
