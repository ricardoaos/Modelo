using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.Domain.Entities.Empresas;
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
    public class EmpresaRepository 
        : EfCoreRepositoryBase<FiscalPrimeDbContext, Empresa>, IEmpresaRepository
    {
        public EmpresaRepository(IDbContextProvider<FiscalPrimeDbContext> dbContextProvider) 
            : base(dbContextProvider)
        {
        }

        public async Task<bool> DeleteAsync(long Id)
        {
            await base.DeleteAsync(p => p.Id == Id);
            return true;
        }

        public async Task<Empresa> FindByIdAsync(long Id) =>        
            await base.FirstOrDefaultAsync(p => p.Id == Id);


        public async Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request) =>
            await base.GetAllAsync<EmpresaDTO>(request);

        public async Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request)
        {
            ListDto<EmpresaDTO> retorno = new ListDto<EmpresaDTO>();
            //Filtro opcional por status
            List<bool> listStatus = new List<bool>();

            if (!request.Ativo.IsNullOrEmpty())
                listStatus = request.Ativo.Split(',').Select(p => p.ToLower().Equals("sim")).ToList();

            return await GetAllAsync<EmpresaDTO>(request,
                p =>
                        (((request.Search.IsNullOrEmpty() || p.Nome.ToLower().Contains(request.Search.ToLower())) && request.Nome.IsNullOrEmpty())
                    || ((request.Nome.IsNullOrEmpty() || p.Nome.ToLower().Contains(request.Nome.ToLower())) && request.Search.IsNullOrEmpty()))
                    && (listStatus.Count() == 0 || listStatus.Contains(p.Ativo))
                    && (!request.CodigoMunicipioIBGE.HasValue || p.CodigoMunicipioIBGE.Equals((int)request.CodigoMunicipioIBGE))
                );

        }

        public async Task<EmpresaDTO> GetAsync(DefaultRequestDto key)
        {
            var entity = await base.GetAsync(key);

            var retorno =  entity.MapTo<EmpresaDTO>();

            if (retorno != null)
            {
                retorno.NomeUFIBGE = (
                                    from c in Context.DominioExterno
                                    where c.IdDominio == (long)Constants.Dominio.COD_UF
                                    && c.Codigo == retorno.CodigoUF.ToString()
                                    select c).FirstOrDefault().Descricao;

                retorno.NomeMunicipioIBGE = (
                                    from c in Context.DominioExterno
                                    where c.IdDominio == (long)Constants.Dominio.COD_MUN
                                    && c.Codigo == retorno.CodigoMunicipioIBGE.ToString()
                                    select c).FirstOrDefault().Descricao;
            }

            return retorno;
        }

        public async Task<Empresa> UpdateAsync(Empresa entity) =>
            await base.UpdateAsync(entity);

    }
}
