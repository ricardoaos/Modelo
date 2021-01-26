using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using FiscalPrime.Backend.Shared;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Repositories;

namespace FiscalPrime.Backend.Domain.Interfaces.Repositories
{
    public interface IDominioExternoRepository : IRepository
    {
        Task<DominioExterno> InsertAsync(DominioExterno entity);
        Task<DominioExterno> UpdateAsync(DominioExterno entity);
        Task<bool> DeleteAsync(long Id);
        Task<IListDto<DominioExternoDTO>> GetAllAsync(SearchRequestAllDTO request);
        Task<IListDto<DominioExternoDTO>> GetAllWithDomain(DominioExternoRequestAllDTO request);
        Task<DominioExternoDTO> GetAsync(DefaultRequestDto key);
        Task<DominioExterno> FindByIdAsync(long Id);
    }
}
