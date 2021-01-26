using FiscalPrime.Backend.Application.Interfaces.Base;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Threading.Tasks;
using Tnf.Dto;
using FiscalPrime.Backend.Shared;

namespace FiscalPrime.Backend.Application.Interfaces
{
    public interface IDominioExternoAppService
    {
        Task<IListDto<DominioExternoDTO>> GetAllWithDomainAsync(DominioExternoRequestAllDTO request);
    }
}
