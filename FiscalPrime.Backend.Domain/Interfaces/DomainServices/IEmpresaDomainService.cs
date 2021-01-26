using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;

namespace FiscalPrime.Backend.Domain.Interfaces.DomainServices
{
    public interface IEmpresaDomainService : IDomainService
    {
        Task<Empresa> InsertAsync(Empresa.Builder builder);
        Task<Empresa> UpdateAsync(Empresa.Builder builder);
        Task<bool> DeleteAsync(long Id);
        Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request);
        Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request);
        Task<EmpresaDTO> GetAsync(DefaultRequestDto key);
        Task<Empresa> FindByIdAsync(long Id);
    }
}
