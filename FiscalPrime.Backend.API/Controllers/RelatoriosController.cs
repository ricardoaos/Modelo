using FiscalPrime.Backend.DTO.DTOs.Reports;
using FiscalPrime.Backend.Reports;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Runtime.Session;

namespace FiscalPrime.Backend.API.Controllers
{
    [Route(WebConstants.RelatoriosRouteName)]
    public class RelatoriosController : TnfController
    {
        private const string _name = "Relatorios";

        private readonly ITnfSession _tnfSession;

        public RelatoriosController(ITnfSession tnfSession)
        {
            _tnfSession = tnfSession;

            try { InitPermissionsReport(); } catch (Exception) { }
        }

        /// <summary>
        /// Método que busca o relatórios com os parametros informados
        /// </summary>
        /// <returns>Retorna view com o relatório</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Index(string report, Filters filtros)
        {
            var model = new ReportModel()
            {
                WebReport = GeradorRelatorios.GerarRelatorio(report, filtros),
            };

            ViewBag.WebReport = model.WebReport;

            return View();
        }

        [TnfAuthorize(new string [] {"Relatorios", "Relatorios.Logs", "Relatorios.Empresas"})]
        private void InitPermissionsReport()
        { 
        
        }
    }
}
