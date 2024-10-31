using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class AntecedenteHigieneOralModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdAntecedenteHigieneOral { get; set; }
        public int IdHiloDental { get; set; }
        public int IdEnjuageBucal { get; set; }
        public int IdSangradoEncia { get; set; }
        public int IdCepilloDental { get; set; }
        public int IdHigieneBucal { get; set; }
        public string? FrecuenciaCepilladoDental { get; set; }
        public int idUsuario {  get; set; }
        public int IdPaciente {  get; set; }

        public async void GuardarAntecedenteHigieneOral(AntecedenteHigieneOralModel objAntecedenteHigieneOral)
        {
            Conexion.Open();
            string Query = @"INSERT INTO antecedentehigieneoral(idHiloDental, idEnjuagueBucal , idSangradoEncia, idCepilloDental, idHigieneBucal, frecuenciaCepilladoDental, idUsuario)
                                                                                                  VALUES(@idHiloDental, @idEnjuagueBucal, @idSangradoEncia, @idCepilloDental, @idHigieneBucal, @frecuenciaCepilladoDental, @idUsuario)";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idHiloDental", objAntecedenteHigieneOral.IdHigieneBucal);
            cmd.Parameters.AddWithValue("@idEnjuagueBucal", objAntecedenteHigieneOral.IdEnjuageBucal);
            cmd.Parameters.AddWithValue("@idSangradoEncia", objAntecedenteHigieneOral.IdSangradoEncia);
            cmd.Parameters.AddWithValue("@idCepilloDental", objAntecedenteHigieneOral.IdCepilloDental);
            cmd.Parameters.AddWithValue("@idHigieneBucal", objAntecedenteHigieneOral.IdHigieneBucal);
            cmd.Parameters.AddWithValue("@frecuenciaCepilladoDental", objAntecedenteHigieneOral.FrecuenciaCepilladoDental);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            await cmd.ExecuteNonQueryAsync();
            Conexion.Close();
        }
        public AntecedenteHigieneOralModel ListaHigieneOral(int idPaciente)
        {
            var objHO = new AntecedenteHigieneOralModel();
            Conexion.Open();
            string Query = @"SELECT AHO.* 
                                        FROM antecedentehigieneoral AHO
                                        INNER JOIN paciente PAC
                                        ON AHO.idAntecedenteHigieneOral = PAC.idAntecedenteHigieneOral
                                        WHERE PAC.idPaciente = @idPaciente";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idPaciente",idPaciente);
            using (var LHO = cmd.ExecuteReader())
            {
                while (LHO.Read())
                {
                    objHO.IdAntecedenteHigieneOral = LHO.GetInt32(0);
                    objHO.IdHiloDental = LHO.GetInt32(1);
                    objHO.IdEnjuageBucal = LHO.GetInt32(2);
                    objHO.IdSangradoEncia = LHO.GetInt32(3);
                    objHO.IdCepilloDental = LHO.GetInt32(4);
                    objHO.IdHigieneBucal = LHO.GetInt32(5);
                    objHO.FrecuenciaCepilladoDental = LHO.GetString(6);
                }
            }
            Conexion.Close();
            return objHO;
        }
        public void ModificarHigieneOral(AntecedenteHigieneOralModel objAH)
        {
            Conexion.Open();
            string Query = @"UPDATE antecedentehigieneoral aho
                                            INNER JOIN paciente pac ON aho.idAntecedenteHigieneOral = pac.idAntecedenteHigieneOral
                                            SET aho.idHiloDental = @idHiloDental,
                                            aho.idEnjuagueBucal = @idEnjuagueBucal,
                                            aho.idSangradoEncia = @idSangradoEncia,
                                            aho.idCepilloDental = @idCepilloDental,
                                            aho.idHigieneBucal = @idHigieneBucal,
                                            aho.frecuenciaCepilladoDental = @frecuenciaCepilladoDental,
                                            aho.idUsuario = @idUsuario
                                            WHERE pac.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand( Query, Conexion );
            cmd.Parameters.AddWithValue("@idHiloDental", objAH.IdHiloDental);
            cmd.Parameters.AddWithValue("@idEnjuagueBucal", objAH.IdEnjuageBucal);
            cmd.Parameters.AddWithValue("@idSangradoEncia", objAH.IdSangradoEncia);
            cmd.Parameters.AddWithValue("@idCepilloDental", objAH.IdCepilloDental);
            cmd.Parameters.AddWithValue("@idHigieneBucal", objAH.IdHigieneBucal);
            cmd.Parameters.AddWithValue("@frecuenciaCepilladoDental", objAH.FrecuenciaCepilladoDental);
            cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
            cmd.Parameters.AddWithValue("@idPaciente", objAH.IdPaciente);
            cmd.ExecuteNonQuery();
            Conexion.Close() ;
        }
        
    }
}
