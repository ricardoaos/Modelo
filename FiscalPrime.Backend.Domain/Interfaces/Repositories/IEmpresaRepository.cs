using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace FiscalPrime.Backend.Domain.Interfaces.Repositories
{
    public interface IEmpresaRepository : IRepository
    {
        Task<Empresa> InsertAsync(Empresa entity);
        Task<Empresa> UpdateAsync(Empresa entity);
        Task<bool> DeleteAsync(long Id);
        Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request);
        Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request);
        Task<EmpresaDTO> GetAsync(DefaultRequestDto key);
        Task<Empresa> FindByIdAsync(long Id);
    }
}
