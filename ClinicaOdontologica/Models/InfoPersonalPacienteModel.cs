using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace ClinicaOdontologica.Models
{
    public class InfoPersonalPacienteModel
    {
       
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdInformacionPersonal { get; set; }
        [Required(ErrorMessage = "Debe ingresar el Nombre.")]
        [StringLength(10, ErrorMessage = "El nombre no puede tener más de 10 caracteres.")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage ="Debe ingresar el Apellido Paterno.")]
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }        
        public string? Ocupacion { get; set; }
        [Required(ErrorMessage = "Debe ingresar una Dirección.")]
        public string? Direccion { get; set; }        
        [Required(ErrorMessage = "Debe ingresar un telefono.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "El teléfono solo puede contener números.")]
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "Debe ingresar su Edad.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "La edad solo debe contener números.")]
        public int Edad { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }
        public int IdEstadoCivil { get; set; }        
        public int IdGradoInstruccion { get; set; }
        public int IdLugarNacimiento { get; set; }
        public int IdSexo { get; set; }
        public string? IdUsuario { get; set; }
        public int IdPaciente { get; set; }

        public List<InfoPersonalPacienteModel> ListaInformacion()
        {
            var objLista = new List<InfoPersonalPacienteModel>();           
            Conexion.Open();
            string Query = "select * from informacionpersonal;";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            var Mlista = cmd.ExecuteReader();
            while (Mlista.Read())
            {
                objLista.Add(new InfoPersonalPacienteModel
                {
                    IdInformacionPersonal = Mlista.GetInt32(0),
                    Nombre = Mlista.GetString(1),
                    ApellidoPaterno = Mlista.GetString(2),
                    ApellidoMaterno = Mlista.GetString(3),
                    Ocupacion = Mlista.GetString(4),
                    Direccion = Mlista.GetString(5),
                    Telefono = Mlista.GetString(6),
                    Edad = Mlista.GetInt32(7),

                });
            }
            return objLista; 
        }

        public InfoPersonalPacienteModel InfoPersonal(int idPaciente)
        {
            var objInfo = new InfoPersonalPacienteModel();            
            Conexion.Open();
            string Query = "SELECT infP.* FROM informacionpersonal infP inner join paciente pac on infP.idInformacionPersonal = pac.idInformacionPersonal where idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idPaciente", idPaciente);
            using (var Mlista = cmd.ExecuteReader())
            {
                while (Mlista.Read())
                {
                    objInfo.IdInformacionPersonal = Convert.ToInt32(Mlista[0].ToString());
                    objInfo.Nombre = Mlista[1].ToString();
                    objInfo.ApellidoPaterno = Mlista[2].ToString();
                    objInfo.ApellidoMaterno = Mlista[3].ToString();
                    objInfo.Ocupacion = Mlista[4].ToString();
                    objInfo.Direccion = Mlista[5].ToString();
                    objInfo.Telefono = Mlista[6].ToString();
                    objInfo.Edad = Convert.ToInt32(Mlista[7].ToString());
                    objInfo.FechaNacimiento = Convert.ToDateTime(Mlista[8]);
                    objInfo.IdEstadoCivil = Convert.ToInt32(Mlista[9]);
                    objInfo.IdGradoInstruccion = Convert.ToInt32(Mlista[10]);
                    objInfo.IdLugarNacimiento = Convert.ToInt32(Mlista[11]);
                    objInfo.IdSexo = Convert.ToInt32(Mlista[12]);
                }
            }               
            Conexion.Close();
            return objInfo; 
        }
        public async Task<bool> GuardarInfoPersonal(InfoPersonalPacienteModel objInfoPersonal)
        {
            bool respuesta;
            try
            {
                Conexion.Open();
                string Query = @"INSERT INTO informacionpersonal(nombre,apellidoPaterno,apellidoMaterno,ocupacion,direccion,telefono,edad,fechaNacimiento,idEstadoCivil,idGradoInstruccion,idLugarNacimiento,idSexo, idUsuario) 
                                                                                            VALUES(@nombre,@apellidoPaterno,@apellidoMaterno,@ocupacion,@direccion,@telefono,@edad,@fechaNacimiento,@idEstadoCivil,@idGradoInstruccion,@idLugarNacimiento,@idSexo, @idUsuario);";
                MySqlCommand cmd = new MySqlCommand(Query, Conexion);
                cmd.Parameters.AddWithValue("@nombre", objInfoPersonal.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", objInfoPersonal.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", objInfoPersonal.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@ocupacion", objInfoPersonal.Ocupacion);
                cmd.Parameters.AddWithValue("@direccion", objInfoPersonal.Direccion);
                cmd.Parameters.AddWithValue("@telefono", objInfoPersonal.Telefono);
                cmd.Parameters.AddWithValue("@edad", objInfoPersonal.Edad);
                cmd.Parameters.AddWithValue("@fechaNacimiento", objInfoPersonal.FechaNacimiento.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@idEstadoCivil", objInfoPersonal.IdEstadoCivil);
                cmd.Parameters.AddWithValue("@idGradoInstruccion", objInfoPersonal.IdGradoInstruccion);
                cmd.Parameters.AddWithValue("@idLugarNacimiento", objInfoPersonal.IdLugarNacimiento);
                cmd.Parameters.AddWithValue("@idSexo", objInfoPersonal.IdSexo);
                cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                await cmd.ExecuteNonQueryAsync();
                respuesta = true;
            }
            catch (Exception)
            {
                respuesta = false;
            }
            finally
            {
                Conexion.Close();
            }
            return respuesta;
        }
        public  void ModificarInfoPersonal(InfoPersonalPacienteModel objInfoPersonal)
        {           
            Conexion.Open();
            string Query = @"UPDATE informacionpersonal ip
                                            INNER JOIN paciente pac ON ip.idInformacionPersonal = pac.idInformacionPersonal
                                            SET ip.nombre = @nombre, ip.apellidoPaterno = @apellidoPaterno, ip.apellidoMaterno = @apellidoMaterno, 
                                                    ip.ocupacion = @ocupacion, ip.direccion = @direccion, 
                                                    ip.telefono = @telefono, ip.edad = @edad, ip.fechaNacimiento = @fechaNacimiento,
                                                    ip.idEstadoCivil = @idEstadoCivil, ip.idGradoInstruccion = @idGradoInstruccion, ip.idLugarNacimiento = @idLugarNacimiento, ip.idSexo = @idSexo, 
                                                    ip.idUsuario = @idUsuario where pac.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand( Query, Conexion);            
            cmd.Parameters.AddWithValue("@nombre", objInfoPersonal.Nombre);
            cmd.Parameters.AddWithValue("@apellidoPaterno", objInfoPersonal.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", objInfoPersonal.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@ocupacion", objInfoPersonal.Ocupacion);
            cmd.Parameters.AddWithValue("@direccion", objInfoPersonal.Direccion);
            cmd.Parameters.AddWithValue("@telefono", objInfoPersonal.Telefono);
            cmd.Parameters.AddWithValue("@edad", objInfoPersonal.Edad);
            cmd.Parameters.AddWithValue("@fechaNacimiento", objInfoPersonal.FechaNacimiento.ToString("yyyy-MM-dd"));
            cmd.Parameters.AddWithValue("@idEstadoCivil", objInfoPersonal.IdEstadoCivil);
            cmd.Parameters.AddWithValue("@idGradoInstruccion", objInfoPersonal.IdGradoInstruccion);
            cmd.Parameters.AddWithValue("@idLugarNacimiento", objInfoPersonal.IdLugarNacimiento);
            cmd.Parameters.AddWithValue("@idSexo", objInfoPersonal.IdSexo);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            cmd.Parameters.AddWithValue("@idPaciente", objInfoPersonal.IdPaciente);
             cmd.ExecuteNonQuery();
           Conexion.Close();
        }
        public bool EliminarInfoPersonal(int InfoPersonal)
        {
            bool respuesta;
            try
            {
                Conexion.Open();
                string Query = "DELETE FROM informacionpersonal where idInformacionPersonal = @idInfopersonal";
                MySqlCommand cmd = new MySqlCommand(Query,Conexion);
                cmd.Parameters.AddWithValue("@idInfopersonal",InfoPersonal);
                cmd.ExecuteNonQuery();
                respuesta = true;
            }
            catch (Exception)
            {
                respuesta= false;
            }
            return respuesta;
        }

    }
}
