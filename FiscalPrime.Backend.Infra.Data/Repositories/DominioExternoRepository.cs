using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.Domain.Interfaces.Repositories;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using FiscalPrime.Backend.Infra.Data.Context;
using FiscalPrime.Backend.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.EntityFrameworkCore;
using Tnf.EntityFrameworkCore.Repositories;

namespace FiscalPrime.Backend.Infra.Data.Repositories
{
    public class DominioExternoRepository 
        : EfCoreRepositoryBase<FiscalPrimeDbContext, DominioExterno>, IDominioExternoRepository
    {
        public DominioExternoRepository(IDbContextProvider<FiscalPrimeDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            await base.DeleteAsync(p => p.Id == Id);
            return true;
        }

        public async Task<DominioExterno> FindByIdAsync(long Id) =>        
            await base.FirstOrDefaultAsync(p => p.Id == Id);


        public async Task<IListDto<DominioExternoDTO>> GetAllAsync(SearchRequestAllDTO request) =>
            await base.GetAllAsync<DominioExternoDTO>(request);
        
        public async Task<IListDto<DominioExternoDTO>> GetAllWithDomain(DominioExternoRequestAllDTO request)
        {
            return await GetAllAsync<DominioExternoDTO>(request,
                p =>
                    p.IdDominio == (long)request.IdDominio
                    && (request.Search.IsNullOrEmpty() || p.Descricao.Contains(request.Search))
                    && (request.UF.IsNullOrEmpty() || p.UF.Contains(request.UF))
                    && (request.Codigo.IsNullOrEmpty() || p.Codigo.Equals(request.Codigo))
                );

        }
        public async Task<DominioExternoDTO> GetAsync(DefaultRequestDto key)
        {
            var entity = await base.GetAsync(key);
            return entity.MapTo<DominioExternoDTO>();
        }

        public async Task<DominioExterno> UpdateAsync(DominioExterno entity) =>
            await base.UpdateAsync(entity);

    }
}
