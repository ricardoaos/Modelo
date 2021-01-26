using FiscalPrime.Backend.Application.Interfaces;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Dto;

namespace FiscalPrime.Backend.API.Controllers
{
    [Route(WebConstants.EmpresaRouteName)]

    [TnfAuthorize(new string[] { "Cadastros", "Cadastros.Empresa" })]
    public class EmpresaController : TnfController
    {
        private readonly IEmpresaAppService _appService;
        private const string _name = "Empresas";

        public EmpresaController(IEmpresaAppService appService)
        {
            _appService = appService;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <param name="requestDto">Request params</param>
        /// <returns>List</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IListDto<EmpresaDTO>), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [TnfAuthorize("Cadastros.Empresa.Consultar")]
        public async Task<IActionResult> GetAll([FromQuery] EmpresaRequestAllDTO requestDto)
        {
            var response = await _appService.GetAllWithFiltersAsync(requestDto);

            return CreateResponseOnGetAll(response, _name);
        }

        /// <summary>
        /// Get product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="requestDto">Request params</param>
        /// <returns>Product requested</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EmpresaDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [TnfAuthorize("Cadastros.Empresa.ConsultarPorId")]
        public async Task<IActionResult> Get(long id, [FromQuery] RequestDto requestDto)
        {
            var request = new DefaultRequestDto(id, requestDto);

            var response = await _appService.GetAsync(request);

            return CreateResponseOnGet(response, _name);
        }

        /// <summary>
        /// Create a new product
        /// </summary>
        /// <param name="dto">Product to create</param>
        /// <returns>Product created</returns>
        [HttpPost]
        [ProducesResponseType(typeof(EmpresaDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [TnfAuthorize("Cadastros.Empresa.Inserir")]
        public async Task<IActionResult> Post([FromBody] EmpresaDTO dto)
        {
            dto = await _appService.CreateAsync(dto);

            return CreateResponseOnPost(dto, _name);
        }


        /// <summary>
        /// Update a product
        /// </summary>
        /// <param name="id">Product id</param>
        /// <param name="dto">Product content to update</param>
        /// <returns>Updated product</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(EmpresaDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [TnfAuthorize("Cadastros.Empresa.Alterar")]
        public async Task<IActionResult> Put(long id, [FromBody] EmpresaDTO dto)
        {
            dto = await _appService.UpdateAsync(id, dto);

            return CreateResponseOnPut(dto, _name);
        }

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">Product id</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [TnfAuthorize("Cadastros.Empresa.Excluir")]
        public async Task<IActionResult> Delete(long id)
        {
            await _appService.DeleteAsync(id);

            return CreateResponseOnDelete(_name);
        }


    }
}
