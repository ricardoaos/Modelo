using FiscalPrime.Backend.Application.Interfaces;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Domain.Interfaces.DomainServices;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System;
using System.Threading.Tasks;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Tnf.Repositories.Uow;

namespace FiscalPrime.Backend.Application.Services
{
    public class EmpresaAppService : ApplicationService, IEmpresaAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IEmpresaDomainService _domainService;

        public EmpresaAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IEmpresaDomainService domainService,
            INotificationHandler notification)
            : base(notification)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _domainService = domainService;
        }

        public async Task<EmpresaDTO> CreateAsync(EmpresaDTO dto)
        {
            if (!ValidateDto<EmpresaDTO>(dto))
                return null;

            var builder = Empresa.Create(Notification)
                .WithAtivo(dto.Ativo)
                .WithBairro(dto.Bairro)
                .WithCep(dto.Cep)
                .WithCodigoCNAE(dto.CodigoCNAE)
                .WithCodigoCRT(dto.CodigoCRT)
                .WithCodigoMunicipioIBGE(dto.CodigoMunicipioIBGE)
                .WithCodigoPais(dto.CodigoPais)
                .WithCodigoSuframa(dto.CodigoSuframa)
                .WithCodigoUF(dto.CodigoUF)
                .WithComplemento(dto.Complemento)
                .WithEndereco(dto.Endereco)
                .WithInscricaoEstadual(dto.InscricaoEstadual)
                .WithInscricaoEstadualST(dto.InscricaoEstadualST)
                .WithInscricaoMunicipal(dto.InscricaoMunicipal)
                .WithNome(dto.Nome)
                .WithNomeFantasia(dto.NomeFantasia)
                .WithNomePais(dto.NomePais)
                .WithNumero(dto.Numero)
                .WithNumeroCNPJCPF(dto.NumeroCNPJCPF)
                .WithTelefone(dto.Telefone)
                .WithDataInclusao(DateTime.Now)
                .WithDataUltimaAlteracao(DateTime.Now);


            Empresa entity = null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                entity = await _domainService.InsertAsync(builder);

                await uow.CompleteAsync().ForAwait();
            }

            if (Notification.HasNotification())
                return null;

            return entity.MapTo<EmpresaDTO>();
        }

        public async Task DeleteAsync(long Id)
        {
            if (!ValidateId(Id))
                return;

            using (var uow = _unitOfWorkManager.Begin())
            {
                await _domainService.DeleteAsync(Id);
                await uow.CompleteAsync().ForAwait();
            }
        }

        public async Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request) =>
            await _domainService.GetAllAsync(request);

        public async Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request)
        {
            return await _domainService.GetAllWithFiltersAsync(request);
        }

        public async Task<EmpresaDTO> GetAsync(DefaultRequestDto id)
        {
            if (!ValidateRequestDto(id) || !ValidateId<long>(id.Id))
                return null;

            return await _domainService.GetAsync(id);
        }

        public async Task<EmpresaDTO> UpdateAsync(long Id, EmpresaDTO dto)
        {
            if (!ValidateDtoAndId(dto, Id))
                return null;

            var builder = Empresa.Create(Notification)
                .WithId(dto.Id)
                .WithAtivo(dto.Ativo)
                .WithBairro(dto.Bairro)
                .WithCep(dto.Cep)
                .WithCodigoCNAE(dto.CodigoCNAE)
                .WithCodigoCRT(dto.CodigoCRT)
                .WithCodigoMunicipioIBGE(dto.CodigoMunicipioIBGE)
                .WithCodigoPais(dto.CodigoPais)
                .WithCodigoSuframa(dto.CodigoSuframa)
                .WithCodigoUF(dto.CodigoUF)
                .WithComplemento(dto.Complemento)
                .WithEndereco(dto.Endereco)
                .WithInscricaoEstadual(dto.InscricaoEstadual)
                .WithInscricaoEstadualST(dto.InscricaoEstadualST)
                .WithInscricaoMunicipal(dto.InscricaoMunicipal)
                .WithNome(dto.Nome)
                .WithNomeFantasia(dto.NomeFantasia)
                .WithNomePais(dto.NomePais)
                .WithNumero(dto.Numero)
                .WithNumeroCNPJCPF(dto.NumeroCNPJCPF)
                .WithDataUltimaAlteracao(DateTime.Now)
                .WithDataInclusao(dto.DataInclusao)
                .WithTelefone(dto.Telefone);

            Empresa entity = null;

            using (var uow = _unitOfWorkManager.Begin())
            {
                entity = await _domainService.UpdateAsync(builder);

                await uow.CompleteAsync().ForAwait();
            }

            if (Notification.HasNotification())
                return null;

            dto.Id = Id;
            return dto;
        }
    }
}
