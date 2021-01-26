using FiscalPrime.Backend.Application.Interfaces;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using FiscalPrime.Backend.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace FiscalPrime.Backend.API.Controllers
{
    [Route(WebConstants.DominioRouteName)]

    public class DominioController : TnfController
    {
        private readonly IDominioExternoAppService _appService;

        public DominioController(IDominioExternoAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <returns>List</returns>
        [HttpGet("{codigo}")]
        [ProducesResponseType(typeof(DominioExternoDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Get([FromRoute] string codigo, [FromQuery] string filter, [FromQuery] Constants.Dominio dominio,  [FromQuery] string uf)
        {

            DominioExternoRequestAllDTO request = new DominioExternoRequestAllDTO
            {
                IdDominio = dominio,
                UF = uf,
                Codigo = codigo
            };

            var response = await _appService.GetAllWithDomainAsync(request);

            return CreateResponseOnGet(response.Items.FirstOrDefault(), WebConstants.DominioRouteName);
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <returns>List</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<DominioExternoDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> GetAll([FromQuery] string filter, [FromQuery] Constants.Dominio dominio, [FromQuery] string uf)
        {

            DominioExternoRequestAllDTO request = new DominioExternoRequestAllDTO
            {
                IdDominio = dominio,
                Page = 1,
                PageSize = 999,
                Search = filter,
                UF = (uf == "undefined" ? null : uf)
            };

            var response = await _appService.GetAllWithDomainAsync(request);

            return CreateResponseOnGetAll(response, WebConstants.DominioRouteName);
        }
    }
}
