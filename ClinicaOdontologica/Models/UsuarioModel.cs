using MySql.Data.MySqlClient;
using System.ComponentModel.DataAnnotations;


namespace ClinicaOdontologica.Models
{
    public class UsuarioModel
    {
       
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());
        public int IdUsuario { get; set; }
        public int IdRol { get; set; }
        public string? Rol {  get; set; }
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo es obligatorio.")]
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatoria.")]
        public string? Password { get; set; }
        public string? ImgUsuario { get; set; }
        public int IdActivo {  get; set; }

        public void GuardarUsuarioModel(UsuarioModel objUsuario)
        {
            Conexion.Open();
            string Query = @"INSERT INTO usuario(idRol, nombre, apellidoPaterno, apellidoMaterno, direccion, telefono,pass, imgUsuario)
                                                                    VALUES(@idRol, @nombre, @apellidoPaterno, @apellidoMaterno, @direccion, @telefono,@pass, @imgUsuario)";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            cmd.Parameters.AddWithValue("@idRol", objUsuario.IdRol);
            cmd.Parameters.AddWithValue("@nombre", objUsuario.Nombre);
            cmd.Parameters.AddWithValue("@apellidoPaterno", objUsuario.ApellidoPaterno);
            cmd.Parameters.AddWithValue("@apellidoMaterno", objUsuario.ApellidoMaterno);
            cmd.Parameters.AddWithValue("@direccion", objUsuario.Direccion);
            cmd.Parameters.AddWithValue("@telefono", objUsuario.Telefono);
            cmd.Parameters.AddWithValue("@pass", objUsuario.Password);
            cmd.Parameters.AddWithValue("@imgUsuario", objUsuario.ImgUsuario);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }      
        public void ModificarUsuario(UsuarioModel objUsuario)
        {
            try
            {
                Conexion.Open();
                string Query = @"UPDATE usuario SET idRol = @idRol, nombre = @nombre, apellidoPaterno = @apellidoPaterno, apellidoMaterno = @apellidoMaterno,
                                                                                direccion = @direccion, telefono = @telefono, pass = @pass,  imgUsuario = @imgUsuario WHERE idUsuario = @IdUsuario;";
                MySqlCommand cmd = new MySqlCommand(Query, Conexion);
                cmd.Parameters.AddWithValue("@idRol", objUsuario.IdRol);
                cmd.Parameters.AddWithValue("@nombre", objUsuario.Nombre);
                cmd.Parameters.AddWithValue("@apellidoPaterno", objUsuario.ApellidoPaterno);
                cmd.Parameters.AddWithValue("@apellidoMaterno", objUsuario.ApellidoMaterno);
                cmd.Parameters.AddWithValue("@direccion", objUsuario.Direccion);
                cmd.Parameters.AddWithValue("@telefono", objUsuario.Telefono);
                cmd.Parameters.AddWithValue("@pass", objUsuario.Password);
                cmd.Parameters.AddWithValue("@IdUsuario", objUsuario.IdUsuario);
                cmd.Parameters.AddWithValue("@imgUsuario", objUsuario.ImgUsuario);
                cmd.ExecuteNonQuery();
            }
            catch (Exception )
            {
                throw;
            }           
            Conexion.Close();
        }
        public List<UsuarioModel> ListaInformacionUsuario()
        {
            var objLista = new List<UsuarioModel>();
            Conexion.Open();
            string Query = @"select us.idUsuario, r.tipoRol, us.nombre, us.apellidoPaterno, us.apellidoMaterno, us.direccion, us.telefono 
                                        from usuario us INNER JOIN rol r ON us.idRol = r.idRol WHERE idActivo = 1;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            var Mlista = cmd.ExecuteReader();
            while (Mlista.Read())
            {
                objLista.Add(new UsuarioModel
                {
                    IdUsuario = Mlista.GetInt32(0),
                    Rol = Mlista.GetString(1),
                    Nombre = Mlista.GetString(2),
                    ApellidoPaterno = Mlista.GetString(3),
                    ApellidoMaterno = Mlista.GetString(4),
                    Direccion = Mlista.GetString(5),
                    Telefono = Mlista.GetString(6)                
                });
            }
            Conexion.Close();
            return objLista;
        }
        public UsuarioModel InfoUsuario(int IdUsuario)
        {
            var objInfoUsuario = new UsuarioModel();
            Conexion.Open();
            string Query = "SELECT * FROM usuario WHERE idUsuario = @idUsuario;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            using (var Mlista = cmd.ExecuteReader())
            {
                while (Mlista.Read())
                {
                    objInfoUsuario.IdUsuario = Mlista.GetInt32(0);
                    objInfoUsuario.IdRol = Mlista.GetInt32(1);
                    objInfoUsuario.Nombre = Mlista.GetString(2);
                    objInfoUsuario.ApellidoPaterno = Mlista.GetString(3);
                    objInfoUsuario.ApellidoMaterno = Mlista.GetString(4);
                    objInfoUsuario.Direccion = Mlista.GetString(5);
                    objInfoUsuario.Telefono = Mlista.GetString(6);
                    objInfoUsuario.Password = Mlista.GetString(7);
                }
            }
            Conexion.Close();
            return objInfoUsuario;
        }       
        public UsuarioModel ValidarUsuario(string apellido, string password)
        {
            UsuarioModel usuario = null;
            try
            {
                Conexion.Open();
                string query = "SELECT idUsuario, idRol, apellidoPaterno, pass, imgUsuario FROM usuario WHERE apellidoPaterno = @Apellido AND pass = @pass AND idActivo = 1;";
                MySqlCommand cmd = new MySqlCommand(query, Conexion);
                cmd.Parameters.AddWithValue("@Apellido", apellido);
                cmd.Parameters.AddWithValue("@pass", password);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usuario = new UsuarioModel
                        {
                            IdUsuario = Convert.ToInt32(reader["idUsuario"]),
                            IdRol = Convert.ToInt32(reader["idRol"]),
                            ApellidoPaterno = reader["apellidoPaterno"].ToString(),
                            Password = reader["pass"].ToString(),
                            ImgUsuario = reader["imgUsuario"].ToString()
                        };
                    }
                }
            }
            finally
            {
                Conexion.Close();
            }
            return usuario;
        }
        public void EliminarUsuario(UsuarioModel objUsuario)
        {
            Conexion.Open();
            string Query = "UPDATE usuario SET idActivo = 0 WHERE idUsuario =@idUsuario;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idUsuario", objUsuario.IdUsuario);
            cmd.ExecuteNonQuery();
            Conexion.Close();
        }
    }
}
