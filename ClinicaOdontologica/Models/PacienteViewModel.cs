using MySql.Data.MySqlClient;
namespace ClinicaOdontologica.Models
{
    public class PacienteViewModel
    {
        MySqlConnection Conexion = new MySqlConnection(new Conexion().getCadenaConexion());

        public int IdUsuario { get; set; }
        public int IdTratamientoMedicoActual { get; set; }
        public int IdExamenOral { get; set; }
        public int IdAntecedenteBucoDental { get; set; }
        public int IdAntecedenteHigieneOral { get; set; }
        public int IdInformacionPersonal { get; set; }
        public int IdAntecedentePatologico { get; set; }
        public int IdPaciente {  get; set; }
        public string? NombrePaciente { get; set; }
        public string? ApellidoPaterno {  get; set; }
        public string? ApellidoMaterno {  get; set; }
        public string? Telefono {  get; set; }

      
        public InfoPersonalPacienteModel? InfoPersonalPacienteModel { get; set; }
        public GradoInstruccionModel? GradoInstruccionModel { get; set; }
        public EstadoCivilModel? EstadoCivilModel { get; set; }
        public SexoModel? SexoModel { get; set; }
        public LugarNacimientoModel? LugarNacimientoModel { get; set; } 
        public AntecedentePatologicoModel? AntecedentePatologicoModel { get; set; }
        public List<EnfermedadModel>? Enfermedades { get; set; }
        public List<int>? EnfermedadesSeleccionadas { get; set; }
        public AlergiaModel? AlergiaModel { get; set; }
        public EmbarazoModel? EmbarazoModel { get; set; }
        public TratamientoMedico? TratamientoMedico { get; set; }
        public ExamenOralModel? ExamenOralModel { get;set; }
        public RespiradorOralModel? RespiradorOralModel { get; set; }
        public ProtesisDentalModel? ProtesisDentalModel { get; set; }   
        public AntecedenteBucoDentalModel? AntecedenteBucoDentalModel { get; set; }
        public BebeModel? BebeModel { get; set; }
        public FumaModel? FumaModel { get; set; }
        public AntecedenteHigieneOralModel? AntecedenteHigieneOralModel { get; set; }
        public CepilloDentalModel? CepilloDentalModel { get; set; }
        public HiloDentalModel? HiloDentalModel { get; set; }
        public EnjuagueBucalModel? EnjuagueBucalModel { get; set; }
        public SangradoEnciaModel? SangradoEnciaModel { get; set; }
        public HigieneBucalModel? HigieneBucalModel { get;  set; }
       
        public async void guardarPacienteModel(PacienteViewModel objPaciente)
        {
            Conexion.Open();
            string Query = @$"INSERT INTO paciente(idUsuario, idTratamientoMedicoActual, idExamenOral, idAntecedenteBucoDental, idAntecedenteHigieneOral, idInformacionPersonal, idAntecedentePatologico, idActivo)
                                                VALUES ( @idUsuario,
                                                (SELECT MAX(idTratamientoMedicoActual) FROM tratamientomedicoactual),
                                                (SELECT MAX(idExamenOral) FROM examenoral),
                                                (SELECT MAX(idAntecedenteBucoDental) FROM antecedentebucodental),
                                                (SELECT MAX(idAntecedenteHigieneOral) FROM antecedentehigieneoral),
                                                (SELECT MAX(idInformacionPersonal) FROM informacionpersonal),
                                                (SELECT MAX(idAntecedentePatologico) FROM antecedentepatologico),
                                                 1);";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idUsuario", IdUsuario);
            await cmd.ExecuteNonQueryAsync();
            Conexion.Close(); 
        }
        public List<PacienteViewModel> ListaPacientes()
        {
            List<PacienteViewModel> LPaciente = new List<PacienteViewModel>(); 
            Conexion.Open();
            string Query = @"SELECT PAC.idPaciente, IP.nombre, IP.apellidoPaterno, IP.apellidoMaterno, IP.telefono
                                            FROM paciente PAC INNER JOIN informacionpersonal IP
                                            ON PAC.idInformacionPersonal = IP.idInformacionPersonal WHERE idActivo = 1";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            using (var reader = cmd.ExecuteReader()) 
            {
                while (reader.Read())
                {
                    LPaciente.Add(new PacienteViewModel
                    {
                        IdPaciente = Convert.ToInt32(reader["idPaciente"]),
                        NombrePaciente = reader["nombre"].ToString(),
                        ApellidoPaterno = reader["apellidoPaterno"].ToString(),
                        ApellidoMaterno = reader["apellidoMaterno"].ToString(),
                        Telefono = reader["telefono"].ToString()
                    }) ;
                }
            }
            Conexion.Close();
            return LPaciente; 
        }
        public void EliminarPaciente(PacienteViewModel objPaciente)
        {
            Conexion.Open();
            string Query = "UPDATE paciente SET idActivo = 0 WHERE idPaciente = @idPaciente;";
            MySqlCommand cmd = new MySqlCommand(Query, Conexion);
            cmd.Parameters.AddWithValue("@idPaciente",objPaciente.IdPaciente);
            cmd.ExecuteNonQuery();
            Conexion.Close() ;
        }
    }
}
