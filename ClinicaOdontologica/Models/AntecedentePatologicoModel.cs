using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using MySql.Data.MySqlClient;

namespace ClinicaOdontologica.Models
{
    public class AntecedentePatologicoModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdAntecedentePatologico { get; set; }
        public string? AntecedenteFamiliar { get; set; }
        public string? Otro { get; set; }
        public string? TipoAlergia { get; set; }
        public int? SemanaEmbarazo { get; set; }
        public int IdEmbarazo { get; set; }
        public int IdAlergia { get; set; }
        public int IdEnfermedad { get; set; }
        public int IdUsuario { get; set; }
        public int IdPaciente {  get; set; }

        public async Task<bool> registrarAntecedetePatologico(AntecedentePatologicoModel objAntecedentePatologico)
        {
            bool registro;
            try
            {
                Conexion.Open();
                string Query = @"INSERT INTO antecedentepatologico(antecedenteFamiliar,otro,tipoAlergia,semanaEmbarazo,idEmbarazo,idAlergia,idUsuario) 
                                                                                                  VALUES(@antecedenteFamiliar,@otro,@tipoAlergia,@semanaEmbarazo,@idEmbarazo,@idAlergia,@idUsuario);";
                MySqlCommand cmd = new MySqlCommand(Query,Conexion);
                cmd.Parameters.AddWithValue("@antecedenteFamiliar", objAntecedentePatologico.AntecedenteFamiliar);
                cmd.Parameters.AddWithValue("@otro", objAntecedentePatologico.Otro);
                cmd.Parameters.AddWithValue("@tipoAlergia", objAntecedentePatologico.TipoAlergia);
                cmd.Parameters.AddWithValue("@semanaEmbarazo", objAntecedentePatologico.SemanaEmbarazo);
                cmd.Parameters.AddWithValue("@idEmbarazo", objAntecedentePatologico.IdEmbarazo);
                cmd.Parameters.AddWithValue("@idAlergia", objAntecedentePatologico.IdAlergia);
                cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                await cmd.ExecuteNonQueryAsync();
                registro = true;
            }
            catch (Exception)
            {
                registro = false;
            }
            finally
            {
                Conexion.Close();
            }
            return registro;
        }       
        public async Task<bool> GuardarAntecedenteEnfermedad(List<int> ListaEnfermedades)
        {
            bool registro;
            try
            {
                Conexion.Open();
                
                foreach (var IdEnfermedad in ListaEnfermedades)
                {
                    string Query = @"INSERT INTO antecedenteenfermedad(idAntecedentePatologico,idEnfermedad)
                                                VALUES((SELECT MAX(idAntecedentePatologico) FROM antecedentepatologico), @idEnfermedad)";
                    MySqlCommand cmd = new MySqlCommand(Query, Conexion);
                    cmd.Parameters.AddWithValue("@idEnfermedad", IdEnfermedad);
                    await cmd.ExecuteNonQueryAsync();
                }              
                registro = true;
            }
            catch (Exception)
            {
                registro = false;               
            }
            finally { Conexion.Close(); }
            return registro;
        }
        public AntecedentePatologicoModel InfoAntecedentePatologico(int idPaciente)
        {
            var objAP = new AntecedentePatologicoModel();
            Conexion.Open();
            string Query = @"SELECT  AP.idAntecedentePatologico, AP.antecedenteFamiliar, AP.otro, AP.tipoAlergia, AP.semanaEmbarazo, AP.idEmbarazo, AP.idAlergia 
                                            FROM antecedentepatologico AP INNER JOIN paciente PAC ON AP.idAntecedentePatologico = PAC.idAntecedentePatologico 
                                            WHERE PAC.idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idPaciente",idPaciente);
            using (var LAP = cmd.ExecuteReader())
            {
                while(LAP.Read())
                {
                    objAP.IdAntecedentePatologico = Convert.ToInt32(LAP["idAntecedentePatologico"]);
                    objAP.AntecedenteFamiliar = LAP["antecedenteFamiliar"].ToString();
                    objAP.Otro = LAP["otro"].ToString();
                    objAP.TipoAlergia = LAP["tipoAlergia"].ToString();
                    objAP.SemanaEmbarazo = LAP["semanaEmbarazo"] != DBNull.Value ? Convert.ToInt32(LAP["semanaEmbarazo"]) : (int?)null;
                    objAP.IdEmbarazo = Convert.ToInt32(LAP["idEmbarazo"]);
                    objAP.IdAlergia = Convert.ToInt32(LAP["idAlergia"]);
                }
            }
            Conexion.Close();
            return objAP;
        }

        public List<int> SeleccionEnfermedades(int idPaciente)
        {
            List<int>? LE = new List<int> ();
            Conexion.Open();
            string Query = @"SELECT ae.idAntecedenteEnfermedad, ae.idAntecedentePatologico, ae.idEnfermedad
                                                FROM antecedenteenfermedad ae
                                                INNER JOIN antecedentepatologico ap
                                                ON ae.idAntecedentePatologico = ap.idAntecedentePatologico
                                                INNER JOIN paciente pac
                                                ON ap.idAntecedentePatologico = pac.idAntecedentePatologico
                                                where pac.idPaciente = @idAP";
            MySqlCommand cmd = new MySqlCommand(Query,Conexion);
            cmd.Parameters.AddWithValue("@idAP", idPaciente);
            using(var objLE = cmd.ExecuteReader())
            {
                while (objLE.Read())
                {
                    LE.Add( Convert.ToInt32(objLE["idEnfermedad"]));
                }
            }
            Conexion.Close();
            return LE;
        }
        public int ModificarAntecedetePatologico(AntecedentePatologicoModel objAntecedentePatologico)
        {
            int registro = 0;
            try
            {
                Conexion.Open();
                string Query = @"UPDATE antecedentepatologico ap
                                            INNER JOIN paciente	pac ON ap.idAntecedentePatologico = pac.idAntecedentePatologico
                                            SET ap.antecedenteFamiliar = @antecedenteFamiliar, 
                                                   ap.otro = @otro, ap.tipoAlergia = @tipoAlergia, ap.semanaEmbarazo = @semanaEmbarazo, 
                                                   ap.idEmbarazo = @idEmbarazo, ap.idAlergia = @idAlergia, ap.idUsuario = @idUsuario
                                            where pac.idPaciente = @idPaciente";
                MySqlCommand cmd = new MySqlCommand(Query, Conexion);
                cmd.Parameters.AddWithValue("@antecedenteFamiliar", objAntecedentePatologico.AntecedenteFamiliar);
                cmd.Parameters.AddWithValue("@otro", objAntecedentePatologico.Otro);
                cmd.Parameters.AddWithValue("@tipoAlergia", objAntecedentePatologico.TipoAlergia);
                cmd.Parameters.AddWithValue("@semanaEmbarazo", objAntecedentePatologico.SemanaEmbarazo);
                cmd.Parameters.AddWithValue("@idEmbarazo", objAntecedentePatologico.IdEmbarazo);
                cmd.Parameters.AddWithValue("@idAlergia", objAntecedentePatologico.IdAlergia);
                cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
                cmd.Parameters.AddWithValue("@idPaciente", objAntecedentePatologico.IdPaciente);
                cmd.ExecuteNonQuery();

                string QueryId = @"SELECT  AP.idAntecedentePatologico FROM antecedentepatologico AP 
                                                INNER JOIN paciente PAC ON AP.idAntecedentePatologico = PAC.idAntecedentePatologico WHERE PAC.idPaciente = @idPaciente;";
                MySqlCommand cmdId = new MySqlCommand(@QueryId, Conexion);
                cmdId.Parameters.AddWithValue("@idPaciente",objAntecedentePatologico.IdPaciente);

                registro = Convert.ToInt16(cmdId.ExecuteScalar());
            }
            catch (Exception)
            {
                registro = 0;
            }
            finally { Conexion.Close(); }
           return registro;
        }

        public async Task<bool> ModificarAntecedenteEnfermedad(List<int> ListaEnfermedades, int IdAntecedentePatologico)
        {
            Conexion.Open();
            using var transaction = Conexion.BeginTransaction();
            try
            {
                foreach (var IdEnfermedad in ListaEnfermedades)
                {
                    string insertQuery = @"INSERT INTO antecedenteenfermedad(idAntecedentePatologico, idEnfermedad)
                                                                                                                    VALUES(@idAntecedentePatologico, @idEnfermedad)
                                                                                                                    ON DUPLICATE KEY UPDATE idEnfermedad = VALUES(idEnfermedad)";
                    MySqlCommand insertCmd = new MySqlCommand(insertQuery, Conexion, transaction);
                    insertCmd.Parameters.AddWithValue("@idAntecedentePatologico", IdAntecedentePatologico);
                    insertCmd.Parameters.AddWithValue("@idEnfermedad", IdEnfermedad);
                    await insertCmd.ExecuteNonQueryAsync();
                }

                if (ListaEnfermedades.Count > 0)
                {
                    string? ids = string.Join(",", ListaEnfermedades);
                    string deleteQuery = @"DELETE FROM antecedenteenfermedad WHERE idAntecedentePatologico = @idAntecedentePatologico
                                                                                                                                            AND idEnfermedad NOT IN (" + ids + ")";
                    MySqlCommand deleteCmd = new MySqlCommand(deleteQuery, Conexion, transaction);
                    deleteCmd.Parameters.AddWithValue("@idAntecedentePatologico", IdAntecedentePatologico);
                    await deleteCmd.ExecuteNonQueryAsync();
                }
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
            }
            finally { Conexion.Close(); }
            return true;
        }





    }
}
