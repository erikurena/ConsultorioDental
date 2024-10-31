using MySql.Data.MySqlClient;
using Mysqlx.Cursor;
using System.ComponentModel.DataAnnotations;

namespace ClinicaOdontologica.Models
{
    public class AgendarCitaModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdAgendarCita { get; set; }       
        public string? NombrePaciente { get; set; }
        public string? TipoConsulta { get; set; }
        [DataType(DataType.Time)]
        public DateTime Horario { get; set; }
        [DataType(DataType.Date)]
        public DateTime Fecha { get; set; }
        public int IdUsuario { get; set; }
        public string? NombreUsurio { get; set; }
        public int IdConsultorioGuardar { get; set; }
        public int IdConsultorioMostrar { get; set; }
        public string? NombreConsultorio { get; set; }

       
        public void GuardarCita(AgendarCitaModel objAgendarCita)
        {
            Conexion.Open();
            string Query = @"INSERT INTO agendarcita(nombrePaciente, tipoConsulta, horario, fecha, idUsuario, idConsultorio)
                                                                            VALUES(@nombrePaciente,@tipoConsulta, @horario,@fecha, @idUsuario, @idConsultorio);";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@nombrePaciente", objAgendarCita.NombrePaciente);
            cmd.Parameters.AddWithValue("@tipoConsulta", objAgendarCita.TipoConsulta);
            cmd.Parameters.AddWithValue("@horario", objAgendarCita.Horario);
            cmd.Parameters.AddWithValue("@fecha", objAgendarCita.Fecha.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@idUsuario", objAgendarCita.IdUsuario);
            cmd.Parameters.AddWithValue("@idConsultorio", objAgendarCita.IdConsultorioGuardar);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }

        public List<AgendarCitaModel> ListaDoctor()
        {
            Conexion.Open();
            List<AgendarCitaModel> ListaDoctor = new List<AgendarCitaModel>();
            string Query = "SELECT idUsuario, concat(nombre,' ',apellidopaterno,' ',ApellidoMaterno) AS doctor FROM usuario WHERE idRol = 1;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            using (var objDoctor = cmd.ExecuteReader())
            {
                while (objDoctor.Read())
                {
                    ListaDoctor.Add(new AgendarCitaModel{
                        IdUsuario = Convert.ToInt32(objDoctor["idUsuario"]),
                        NombreUsurio = objDoctor["doctor"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaDoctor;
        }
        public List<AgendarCitaModel> ListaConsultorio()
        {
            Conexion.Open();
            List<AgendarCitaModel> ListaConsultorio = new List<AgendarCitaModel>();
            string Query = "SELECT idConsultorio, consultorio FROM consultorio;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var objConsultorio = cmd.ExecuteReader())
            {
                while (objConsultorio.Read())
                {
                    ListaConsultorio.Add(new AgendarCitaModel
                    {
                        IdConsultorioMostrar = Convert.ToInt32(objConsultorio["idConsultorio"]),
                        NombreConsultorio = objConsultorio["consultorio"].ToString()
                    });
                }
            }
            Conexion.Close();
            return ListaConsultorio;
        }
        public List<AgendarCitaModel> ListaConsulta(AgendarCitaModel objConsultorio)
        {
            List<AgendarCitaModel> ListaAgenda = new List<AgendarCitaModel>();
            Conexion.Open();
            string Query = " SELECT idAgendarCita,nombrePaciente,tipoConsulta,horario,fecha FROM agendarcita WHERE idConsultorio = @idConsultorio;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idConsultorio", objConsultorio.IdConsultorioMostrar);
            using (var objAgenda = cmd.ExecuteReader())
            {
                while (objAgenda.Read()) {
                    ListaAgenda.Add(new AgendarCitaModel
                    {
                        IdAgendarCita = Convert.ToInt32(objAgenda["idAgendarCita"]),
                        NombrePaciente = objAgenda["nombrePaciente"].ToString(),
                        TipoConsulta = objAgenda["tipoConsulta"].ToString(),
                        Horario = Convert.ToDateTime(objAgenda["horario"]),
                        Fecha = Convert.ToDateTime(objAgenda["fecha"]),
                    });
                }
            }
            Conexion.Close();
            return ListaAgenda;                
        }
        public void EliminarCita(int IdCita)
        {
            Conexion.Open();
            string Query = "DELETE FROM agendarcita WHERE idAgendarCita = @idAgendarCita;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idAgendarCita",IdCita);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }

    }
}
