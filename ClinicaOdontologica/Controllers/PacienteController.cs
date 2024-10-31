using ClinicaOdontologica.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ClinicaOdontologica.Controllers
{
    [Authorize]
    public class PacienteController : Controller
    {
        InfoPersonalPacienteModel ObjInfoPersonal = new InfoPersonalPacienteModel();
        AntecedentePatologicoModel ObjAntecedentePatologico = new AntecedentePatologicoModel();
        TratamientoMedico ObjTratamientoMedico = new TratamientoMedico();
        ExamenOralModel ObjExamenOral = new ExamenOralModel();
        AntecedenteBucoDentalModel ObjBucoDental = new AntecedenteBucoDentalModel();
        AntecedenteHigieneOralModel ObjHigieneOral = new AntecedenteHigieneOralModel();
        EnfermedadModel ObjEnfermedad = new EnfermedadModel();

        public ActionResult GradoInstruccion()
        {
            var GInstruccion = new GradoInstruccionModel().MostrarInstruccion();
            ViewBag.Instruccion = new SelectList(GInstruccion, "IdGradoInstruccion", "GradoInstruccion");
            return View();
        }
        public ActionResult EstadoCivil()
        {
            var ECivil = new EstadoCivilModel().MostrarEstadoCivil();
            ViewBag.EstadoCivil = new SelectList(ECivil, "IdEstadoCivil", "DescripcionEstCivil");
            return View();
        }
        public ActionResult Sexo()
        {
            var Sexo = new SexoModel().MostrarSexo();
            ViewBag.MostrarSexo = new SelectList(Sexo, "IdSexo", "Sexo");
            return View();
        }
        public ActionResult LugarNacimiento()
        {
            var LNacimiento = new LugarNacimientoModel().MostrarLugarNacimiento();
            ViewBag.MostrarLugarNacimiento = new SelectList(LNacimiento, "IdLugarNacimiento", "LugarNacimiento");
            return View();
        }
        public IActionResult MostrarEnfermedad()
        {
            var Enfermedades = new EnfermedadModel().MostrarEnfermedad();
            return View(Enfermedades);
        }
        public ActionResult MostrarAlergia()
        {
            var objAlergia = new AlergiaModel().MostrarAlergia();
            ViewBag.MostrarAlergia = new SelectList(objAlergia, "IdAlergia", "Alergia");
            return View();
        }
        public ActionResult MostrarEmbarazo()
        {
            var objEmbarazo = new EmbarazoModel().MostrarEmbarazo();
            ViewBag.MostrarEmbarazo = new SelectList(objEmbarazo, "IdEmbarazo", "Embarazo");
            return View();
        }
        public ActionResult MostrarHemorragia()
        {
            var objHemorragia = new HemorragiaDental().MostrarHemorragia();
            ViewBag.MostrarHemorragia = new SelectList(objHemorragia, "IdHemorragiaDental", "Hemorragia");
            return View();
        }
        public ActionResult MostrarRespiradorOral()
        {
            var objRespiradorOral = new RespiradorOralModel().ListaRespiradorOral();
            ViewBag.MostrarRespiradorOral = new SelectList(objRespiradorOral, "IdRespiradorOral", "TipoRespirador");
            return View();
        }
        public ActionResult MostrarProtesisDental()
        {
            var objProtesisDental = new ProtesisDentalModel().ListaProtesisDental();
            ViewBag.MostrarProtesisDental = new SelectList(objProtesisDental, "IdProtesisDental", "ProtesisDental");
            return View();
        }
        public ActionResult MostrarBebe()
        {
            var objBebe = new BebeModel().ListaBebe();
            ViewBag.MostrarBebe = new SelectList(objBebe, "IbBebe", "DescripcionBebe");
            return View();
        }
        public ActionResult MostrarFuma()
        {
            var objFuma = new FumaModel().ListaFuma();
            ViewBag.MostrarFuma = new SelectList(objFuma, "IdFuma", "DescipcionFuma");
            return View();
        }
        public ActionResult MostrarCepilloDental()
        {
            var objCepilloDental = new CepilloDentalModel().ListaCepilloDental();
            ViewBag.MostrarCepilloDental = new SelectList(objCepilloDental, "IdCepilloDental", "UsaCepilloDental");
            return View();
        }
        public ActionResult MostrarHiloDental()
        {
            var objHiloDental = new HiloDentalModel().ListaHiloDental();
            ViewBag.MostrarHiloDental = new SelectList(objHiloDental, "IdHiloDental", "UsaHiloDental");
            return View();
        }
        public ActionResult MostrarEnjuagueBucal()
        {
            var objEnjuagueBucal = new EnjuagueBucalModel().ListaEnjuageBucal();
            ViewBag.MostrarEnjuagueBucal = new SelectList(objEnjuagueBucal, "IdEnjuagueBucal", "UsaEnjuagueBucal");
            return View();
        }
        public ActionResult MostrarSangradoEncia()
        {
            var objSangradoEncia = new SangradoEnciaModel().ListaSangradoEncia();
            ViewBag.MostrarangradoEncia = new SelectList(objSangradoEncia, "IdSangradoEncia", "TieneSangradoEncia");
            return View();
        }
        public ActionResult MostrarHigieneBucal()
        {
            var objHigieneBucal = new HigieneBucalModel().ListaHigieneBucal();
            ViewBag.MostrarHigieneBucal = new SelectList(objHigieneBucal, "IdHigieneBucal", "DescripcionHigieneBucal");
            return View();
        }
        public ActionResult Pacientes()
        {
            var objLista = new PacienteViewModel().ListaPacientes();
            return View(objLista);
        }
        public  ActionResult FuncionesAuxiliares()
        {
             GradoInstruccion();
            EstadoCivil();
            Sexo();
            LugarNacimiento();
            MostrarAlergia();
            MostrarEmbarazo();
            MostrarHemorragia();
            MostrarRespiradorOral();
            MostrarProtesisDental();
            MostrarBebe();
            MostrarFuma();
            MostrarCepilloDental();
            MostrarHiloDental();
            MostrarEnjuagueBucal();
            MostrarSangradoEncia();
            MostrarHigieneBucal();
             return  View();
        }
        public  IActionResult GuardarPaciente()
        {
             FuncionesAuxiliares();
            var model = new PacienteViewModel
            {               
                Enfermedades = ObjEnfermedad.MostrarEnfermedad(),
                EnfermedadesSeleccionadas = new List<int>()
            };            
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> GuardarPacienteAsync(PacienteViewModel objHistorialPaciente)
        {
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ObjInfoPersonal.IdUsuario = idUsuario;
            ObjAntecedentePatologico.IdUsuario = Convert.ToInt32(idUsuario);
            ObjTratamientoMedico.IdUsuario = Convert.ToInt32(idUsuario);
            ObjExamenOral.IdUsuario = Convert.ToInt32(idUsuario);
            ObjBucoDental.IdUsuario = Convert.ToInt32(idUsuario);
            ObjHigieneOral.idUsuario = Convert.ToInt32(idUsuario);
            objHistorialPaciente.IdUsuario = Convert.ToInt32(idUsuario);

            ObjInfoPersonal.GuardarInfoPersonal(objHistorialPaciente.InfoPersonalPacienteModel);
            bool registro = await ObjAntecedentePatologico.registrarAntecedetePatologico(objHistorialPaciente.AntecedentePatologicoModel);
            ObjTratamientoMedico.GuardarTratamientoMedico(objHistorialPaciente.TratamientoMedico);
            ObjExamenOral.GuardarExamenOral(objHistorialPaciente.ExamenOralModel);
            ObjBucoDental.GuardarAntecedenteBucoDental(objHistorialPaciente.AntecedenteBucoDentalModel);
            ObjHigieneOral.GuardarAntecedenteHigieneOral(objHistorialPaciente.AntecedenteHigieneOralModel);
            objHistorialPaciente.guardarPacienteModel(objHistorialPaciente);

            if (registro)
            {
                new AntecedentePatologicoModel().GuardarAntecedenteEnfermedad(objHistorialPaciente.EnfermedadesSeleccionadas);
                return RedirectToAction("Pacientes", "Paciente");
            }
            else
                return View();
        }
       
        public   IActionResult ModificarPaciente(int idPaciente) 
        {
            FuncionesAuxiliares();
           
            var objIPersonal = ObjInfoPersonal.InfoPersonal(idPaciente);
            var objAntecedenteP = ObjAntecedentePatologico.InfoAntecedentePatologico(idPaciente);
            var ObjES = ObjAntecedentePatologico.SeleccionEnfermedades(idPaciente);
            var objTM = ObjTratamientoMedico.InfoTratamientoMedico(idPaciente);
            var objEO = ObjExamenOral.ListaExamenOral(idPaciente);
            var objAB = ObjBucoDental.ListaBucoDental(idPaciente);
            var objHO = ObjHigieneOral.ListaHigieneOral(idPaciente); 

            var model = new PacienteViewModel
            {
                Enfermedades = ObjEnfermedad.MostrarEnfermedad(),
                EnfermedadesSeleccionadas = new List<int>(ObjES),
                InfoPersonalPacienteModel = objIPersonal,
                AntecedentePatologicoModel = objAntecedenteP,
                TratamientoMedico = objTM,
                ExamenOralModel = objEO,
                AntecedenteBucoDentalModel = objAB,
                AntecedenteHigieneOralModel = objHO,
                IdPaciente = idPaciente
            };
            return View(model); 
        }
        [HttpPost]
        public async Task<IActionResult> ModificarPaciente(PacienteViewModel objPaciente)
        {
            FuncionesAuxiliares();
            var idUsuario = User.FindFirstValue(ClaimTypes.NameIdentifier);
          
            ObjInfoPersonal.IdUsuario = idUsuario;
            ObjAntecedentePatologico.IdUsuario = Convert.ToInt32(idUsuario);
            ObjTratamientoMedico.IdUsuario = Convert.ToInt32(idUsuario);
            ObjExamenOral.IdUsuario = Convert.ToInt32(idUsuario);
            ObjBucoDental.IdUsuario = Convert.ToInt32(idUsuario);
            ObjHigieneOral.idUsuario = Convert.ToInt32(idUsuario);
            objPaciente.IdUsuario = Convert.ToInt32(idUsuario);

            ObjInfoPersonal.ModificarInfoPersonal(objPaciente.InfoPersonalPacienteModel);
            var registro = ObjAntecedentePatologico.ModificarAntecedetePatologico(objPaciente.AntecedentePatologicoModel);
            ObjTratamientoMedico.ModificarTratamientoMedico(objPaciente.TratamientoMedico);
            ObjExamenOral.ModificarExamenOral(objPaciente.ExamenOralModel);
            ObjBucoDental.ModificarAntecedenteBucoDental(objPaciente.AntecedenteBucoDentalModel);
            ObjHigieneOral.ModificarHigieneOral(objPaciente.AntecedenteHigieneOralModel);

            if (registro != 0)
            {
                await new AntecedentePatologicoModel().ModificarAntecedenteEnfermedad(objPaciente.EnfermedadesSeleccionadas,registro);
                objPaciente.Enfermedades = ObjEnfermedad.MostrarEnfermedad();
                return RedirectToAction("Pacientes", "Paciente");              
            }
            else {
                objPaciente.Enfermedades = ObjEnfermedad.MostrarEnfermedad();
                return View(objPaciente);
            }
        }
       
       [HttpPost]
        public ActionResult EliminarPaciente(PacienteViewModel objPaciente)
        {
            new PacienteViewModel().EliminarPaciente(objPaciente);
            return RedirectToAction(nameof(Pacientes));
        }
    }
}
