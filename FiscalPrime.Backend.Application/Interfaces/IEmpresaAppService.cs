using FiscalPrime.Backend.Application.Interfaces.Base;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Threading.Tasks;
using Tnf.Dto;

namespace FiscalPrime.Backend.Application.Interfaces
{
    public interface IEmpresaAppService : IGenericAppService<EmpresaDTO>
    {
        Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request);
    }
}
