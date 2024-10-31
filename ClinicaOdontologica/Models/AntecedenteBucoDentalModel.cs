using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;

namespace ClinicaOdontologica.Models
{
    public class AntecedenteBucoDentalModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdAntecedenteBucoDental { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UltimaVisitaOdontologo { get; set; }
        public string? Otro { get; set; }
        public int IdFuma { get; set; }
        public int IdBebe { get; set; }
        public int IdUsuario {  get; set; }
        public int IdPaciente {  get; set; }    

        public async void GuardarAntecedenteBucoDental(AntecedenteBucoDentalModel objAntecedenteBucoDental)
        {
            Conexion.Open();
            string Query = @"INSERT INTO antecedentebucodental(ultimaVisitaOdontologo, Otro, idFuma, iBbebe, idUsuario) 
                                                                                               VALUES(@ultimaVisitaOdontologo, @Otro, @idFuma, @iBbebe, @idUsuario)";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            cmd.Parameters.AddWithValue("@ultimaVisitaOdontologo", objAntecedenteBucoDental.UltimaVisitaOdontologo);
            cmd.Parameters.AddWithValue("@Otro", objAntecedenteBucoDental.Otro);
            cmd.Parameters.AddWithValue("@idFuma", objAntecedenteBucoDental.IdFuma);
            cmd.Parameters.AddWithValue("@iBbebe", objAntecedenteBucoDental.IdBebe);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            await cmd.ExecuteNonQueryAsync();
            Conexion.Close();
        }
        public AntecedenteBucoDentalModel ListaBucoDental(int idPaciente)
        {
            var objBucoDental = new AntecedenteBucoDentalModel();
            Conexion.Open() ;
            string Query = @"SELECT AB.* 
                                        FROM antecedentebucodental AB
                                        INNER JOIN paciente PAC
                                        ON AB.idAntecedenteBucoDental = PAC.idAntecedenteBucoDental
                                        WHERE PAC.idPaciente = @idPaciente";
            MySqlCommand cmd = new MySqlCommand( Query,Conexion);
            cmd.Parameters.AddWithValue ("@idPaciente", idPaciente);
            using (var LBD = cmd.ExecuteReader())
            {
                while (LBD.Read())
                {
                    objBucoDental.IdAntecedenteBucoDental = LBD.GetInt32(0);
                    objBucoDental.UltimaVisitaOdontologo = LBD.GetDateTime(1);
                    objBucoDental.Otro = LBD[2] != DBNull.Value ? Convert.ToString(LBD[2] ) : (string?)null;
                    objBucoDental.IdBebe = LBD.GetInt32(3);
                    objBucoDental.IdFuma = LBD.GetInt32(4);
                }
            }
            Conexion.Close();
            return objBucoDental;
        }
        public void ModificarAntecedenteBucoDental(AntecedenteBucoDentalModel objAB)
        {
            Conexion.Open();
            string Query = @"UPDATE antecedentebucodental ab
                                        INNER JOIN paciente pac ON ab.idAntecedenteBucoDental = pac.idAntecedenteBucoDental
                                        SET ab.ultimaVisitaOdontologo = @ultimaVisitaOdontologo,
                                        ab.Otro = @Otro,
                                        ab.idFuma = @idFuma,
                                        ab.iBbebe = @iBbebe,
                                        ab.idUsuario = @idUsuario
                                        WHERE pac.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            cmd.Parameters.AddWithValue("@ultimaVisitaOdontologo", objAB.UltimaVisitaOdontologo);
            cmd.Parameters.AddWithValue("@Otro", objAB.Otro);
            cmd.Parameters.AddWithValue("@idFuma", objAB.IdFuma);
            cmd.Parameters.AddWithValue("@iBbebe", objAB.IdBebe);
            cmd.Parameters.AddWithValue("@idUsuario", objAB.IdUsuario);
            cmd.Parameters.AddWithValue("@idPaciente", objAB.IdPaciente);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }
    }
}
