using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Runtime.Session;

namespace FiscalPrime.Backend.API.Controllers
{
    [Produces("application/json")]
    [Route(WebConstants.DocumentoFiscalRouteName)]
    [TnfAuthorize("DocumentoFiscal")]
    public class DocumentoFiscalController : TnfController
    {
        private readonly ITnfSession _tnfSession;

        public DocumentoFiscalController(ITnfSession tnfSession)
        {
            _tnfSession = tnfSession;
        }

        [HttpGet("consultadocumentofiscal")]
        [TnfAuthorize("DocumentoFiscal.Consulta")]
        public IActionResult GetConsultaDocumentoFiscal()
        {
            return Ok("DocumentoFiscal.ConsultaDocumentoFiscal");
        }

        [HttpGet("auditoriadocumentofiscal")]
        [TnfAuthorize("DocumentoFiscal.Auditoria")]
        public IActionResult GetAuditoriaDocumentoFiscal()
        {
            return Ok("DocumentoFiscal.AuditoriaDocumentoFiscal");
        }
    }
}
