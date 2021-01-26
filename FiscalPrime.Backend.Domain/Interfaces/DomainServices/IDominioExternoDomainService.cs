using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using FiscalPrime.Backend.Shared;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace FiscalPrime.Backend.Domain.Interfaces.DomainServices
{
    public interface IDominioExternoDomainService : IDomainService
    {
        Task<IListDto<DominioExternoDTO>> GetAllWithDomain(DominioExternoRequestAllDTO request);
    }
}
