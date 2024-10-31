using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class ExamenOralModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdExamenOral { get; set; }
        public string? Atm { get; set; }
        public string? GanglioLinfatico { get; set; }
        public string? Otro { get; set; }
        public string? Labio { get; set; }
        public string? Lengua { get; set; }
        public string? Paladar { get; set; }
        public string? PisoBoca { get; set; }
        public string? MucosaYugal { get; set; }
        public string? Encia { get; set; }
        public int IdRespiradorOral { get; set; }
        public int IdProtesisDental { get; set; }
        public int IdUsuario {  get; set; }
        public int IdPaciente {get; set; }

        public async void GuardarExamenOral(ExamenOralModel ObjExamenOral)
        {
            Conexion.Open();
            string Query = @"INSERT INTO examenoral(atm,GanglioLinfatico,otro,Labio,Lengua,paladar,PisodeBoca,MucosaYugal,Encia,idRespiradorOral,IdProtesisDental, idUsuario)
                                                                            VALUES(@atm, @GanglioLinfatico, @otro, @Labio, @Lengua, @paladar, @PisodeBoca, @MucosaYugal, @Encia, @idRespiradorOral, @idProtesisDental, @idUsuario)";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);           
            cmd.Parameters.AddWithValue("@atm", ObjExamenOral.Atm);
            cmd.Parameters.AddWithValue("@GanglioLinfatico", ObjExamenOral.GanglioLinfatico);
            cmd.Parameters.AddWithValue("@otro", ObjExamenOral.Otro);
            cmd.Parameters.AddWithValue("@Labio", ObjExamenOral.Labio);
            cmd.Parameters.AddWithValue("@Lengua", ObjExamenOral.Lengua);
            cmd.Parameters.AddWithValue("@paladar", ObjExamenOral.Paladar);
            cmd.Parameters.AddWithValue("@PisodeBoca", ObjExamenOral.PisoBoca);
            cmd.Parameters.AddWithValue("@MucosaYugal", ObjExamenOral.MucosaYugal);
            cmd.Parameters.AddWithValue("@Encia", ObjExamenOral.Encia);
            cmd.Parameters.AddWithValue("@idRespiradorOral", ObjExamenOral.IdRespiradorOral);
            cmd.Parameters.AddWithValue("@idProtesisDental", ObjExamenOral.IdProtesisDental);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            await cmd.ExecuteNonQueryAsync();
            Conexion.Close();
        }
        public ExamenOralModel ListaExamenOral(int idPaciente)
        {
            var objEO = new ExamenOralModel();
            Conexion.Open();
            string Query = @"SELECT EO.* FROM examenoral EO INNER JOIN paciente PAC ON EO.idExamenOral = PAC.idExamenOral
                                        WHERE PAC.idPaciente = @idPaciente";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idPaciente",idPaciente);
            using (var LEO = cmd.ExecuteReader())
            {
                while (LEO.Read())
                {
                    objEO.IdExamenOral = LEO.GetInt32(0);
                    objEO.Atm = LEO.GetString(1);
                    objEO.GanglioLinfatico = LEO.GetString(2);
                    objEO.Otro = LEO[3] != DBNull.Value ? Convert.ToString(LEO[3]) : (string?)null;
                    objEO.Labio = LEO.GetString(4);
                    objEO.Lengua = LEO.GetString(5);
                    objEO.Paladar = LEO.GetString(6);
                    objEO.PisoBoca = LEO.GetString(7);
                    objEO.MucosaYugal = LEO.GetString(8);
                    objEO.Encia = LEO.GetString(9);
                    objEO.IdRespiradorOral = LEO.GetInt32(10);
                    objEO.IdProtesisDental = LEO.GetInt32(11);
                }
            }
            Conexion.Close();
            return objEO;
        }
        public void ModificarExamenOral(ExamenOralModel objExamenOral)
        {
            Conexion.Open();
            string Query = @"UPDATE examenoral eo
                                    INNER JOIN paciente pac ON eo.idExamenOral = pac.idExamenOral
                                    SET eo.atm = @atm,
	                                    eo.GanglioLinfatico = @GanglioLinfatico,
                                        eo.otro = @otro,
                                        eo.Labio = @Labio,
                                        eo.Lengua = @Lengua,
                                        eo.paladar = @paladar,
                                        eo.PisodeBoca = @PisodeBoca,
                                        eo.MucosaYugal = @MucosaYugal,
                                        eo.Encia = @Encia,
                                        eo.idRespiradorOral = @idRespiradorOral,
                                        eo.IdProtesisDental = @IdProtesisDental,
                                        eo.idUsuario = @idUsuario
                                    WHERE pac.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@atm", objExamenOral.Atm);
            cmd.Parameters.AddWithValue("@GanglioLinfatico", objExamenOral.GanglioLinfatico);
            cmd.Parameters.AddWithValue("@otro", objExamenOral.Otro);
            cmd.Parameters.AddWithValue("@Labio", objExamenOral.Labio);
            cmd.Parameters.AddWithValue("@Lengua", objExamenOral.Lengua);
            cmd.Parameters.AddWithValue("@paladar", objExamenOral.Paladar);
            cmd.Parameters.AddWithValue("@PisodeBoca", objExamenOral.PisoBoca);
            cmd.Parameters.AddWithValue("@MucosaYugal", objExamenOral.MucosaYugal);
            cmd.Parameters.AddWithValue("@Encia", objExamenOral.Encia);
            cmd.Parameters.AddWithValue("@idRespiradorOral", objExamenOral.IdRespiradorOral);
            cmd.Parameters.AddWithValue("@IdProtesisDental", objExamenOral.IdProtesisDental);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            cmd.Parameters.AddWithValue("@idPaciente", objExamenOral.IdPaciente);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }
    }
}
