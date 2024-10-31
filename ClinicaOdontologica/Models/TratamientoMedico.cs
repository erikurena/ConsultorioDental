using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class TratamientoMedico
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdTratamientoMedicoActual { get; set; }
        public string? Tratamiento { get; set; }
        public string? Medicamento { get; set; }
        public int IdHemorragiaDental { get; set; }
        public bool HemorragiaMediata { get; set; }
        public bool HemorragiaInmediata { get; set; }
        public int IdUsuario {  get; set; }
        public int IdPaciente {  get; set; }


        public async void GuardarTratamientoMedico(TratamientoMedico objTratamientoMedico)
        {
            Conexion.Open();
            string Query = @"INSERT INTO tratamientomedicoactual(tratamiento, medicamento, idHemorragiaDental,idUsuario,hemorragiaInmediata,hemorragiaMediata)  
                                                                                                    VALUES(@tratamiento,@medicamento,@idHemorragiaDental, @idUsuario,@hemorragiaInmediata,@hemorragiaMediata);";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            cmd.Parameters.AddWithValue("@tratamiento", objTratamientoMedico.Tratamiento);
            cmd.Parameters.AddWithValue("@medicamento", objTratamientoMedico.Medicamento);
            cmd.Parameters.AddWithValue("@idHemorragiaDental", objTratamientoMedico.IdHemorragiaDental);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            cmd.Parameters.AddWithValue("@hemorragiaInmediata", objTratamientoMedico.HemorragiaInmediata);
            cmd.Parameters.AddWithValue("@hemorragiaMediata", objTratamientoMedico.HemorragiaMediata);
            await cmd.ExecuteNonQueryAsync();
            Conexion.Close();
        }

        public TratamientoMedico InfoTratamientoMedico(int idPaciente)
        {
            var objTratamientoMedico = new TratamientoMedico();
             Conexion.Open();
            string Query = @"SELECT tma.* 
                                        FROM tratamientomedicoactual tma
                                        INNER JOIN paciente pac
                                        ON tma.idTratamientoMedicoActual = pac.idTratamientoMedicoActual
                                        WHERE pac.idPaciente = @idPaciente";
            MySqlCommand cmd = new MySqlCommand( Query,Conexion);
            cmd.Parameters.AddWithValue("@idPaciente",idPaciente);
            using (var LTM = cmd.ExecuteReader())
            {
                while (LTM.Read())
                {
                    objTratamientoMedico.IdTratamientoMedicoActual = LTM.GetInt32(0);
                    objTratamientoMedico.Tratamiento = LTM.GetString(1);
                    objTratamientoMedico.Medicamento = LTM.GetString(2);
                    objTratamientoMedico.IdHemorragiaDental = LTM.GetInt32(3);
                    objTratamientoMedico.IdUsuario = LTM.GetInt32(4);
                    objTratamientoMedico.HemorragiaInmediata = LTM.GetBoolean(5);
                    objTratamientoMedico.HemorragiaMediata = LTM.GetBoolean(6);
                }
            }
            Conexion.Close();
            return objTratamientoMedico;
        }
        public void ModificarTratamientoMedico(TratamientoMedico objTratamientoMedico) 
        {
            Conexion.Open();
            string Query = @"    UPDATE tratamientomedicoactual tma
                                                INNER JOIN paciente pac ON tma.idTratamientoMedicoActual = pac.idTratamientomedicoActual
                                                SET tma.tratamiento = @tratamiento, 
                                                tma.medicamento = @medicamento,
                                                tma.idHemorragiaDental = @idHemorragiaDental,
                                                tma.idUsuario = @idUsuario,
                                                tma.hemorragiaInmediata = @hemorragiaInmediata,
                                                tma.hemorragiaMediata = @hemorragiaMediata where pac.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@tratamiento",objTratamientoMedico.Tratamiento);         
            cmd.Parameters.AddWithValue("@medicamento",objTratamientoMedico.Medicamento);
            cmd.Parameters.AddWithValue("@idHemorragiaDental",objTratamientoMedico.IdHemorragiaDental);
            cmd.Parameters.AddWithValue("@idUsuario",IdUsuario);
            cmd.Parameters.AddWithValue("@hemorragiaInmediata",objTratamientoMedico.HemorragiaInmediata);
            cmd.Parameters.AddWithValue("@hemorragiaMediata",objTratamientoMedico.HemorragiaMediata);
            cmd.Parameters.AddWithValue("@idPaciente",objTratamientoMedico.IdPaciente);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }

    }
}
