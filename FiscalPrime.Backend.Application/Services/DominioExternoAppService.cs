using FiscalPrime.Backend.Application.Interfaces;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Domain.Interfaces.DomainServices;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using FiscalPrime.Backend.Shared;
using System;
using System.Threading.Tasks;
using System.Linq;
using Tnf.Application.Services;
using Tnf.Dto;
using Tnf.Notifications;
using Tnf.Repositories.Uow;


namespace FiscalPrime.Backend.Application.Services
{
    public class DominioExternoAppService : ApplicationService, IDominioExternoAppService
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IDominioExternoDomainService _domainService;

        public DominioExternoAppService(
            IUnitOfWorkManager unitOfWorkManager,
            IDominioExternoDomainService domainService,
            INotificationHandler notification)
            : base(notification)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _domainService = domainService;
        }

        public async Task<IListDto<DominioExternoDTO>> GetAllWithDomainAsync(DominioExternoRequestAllDTO request)
        {
            var retorno = await _domainService.GetAllWithDomain(request);
            retorno.Items = retorno.Items.OrderBy(p => p.Descricao).ToList();
            return retorno;
        }
    }
}
