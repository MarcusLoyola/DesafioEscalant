using DesafioEscalantHospitalCentral.Models;
using DesafioEscalantHospitalCentral.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DesafioEscalantHospitalCentral.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var model = new AmostraPacienteVirusViewModel() { AmostraEntrada = form["amostra_entrada"] };

            var lab = new Laboratorio();
            lab.ProcessaAmostra(model);

            return Json(lab.Resultado);
        }
    }
}