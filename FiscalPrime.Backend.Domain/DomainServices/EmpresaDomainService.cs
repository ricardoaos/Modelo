using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Domain.Interfaces.DomainServices;
using FiscalPrime.Backend.Domain.Interfaces.Repositories;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Threading.Tasks;
using Tnf.Domain.Services;
using Tnf.Dto;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Domain.DomainServices
{
    public class EmpresaDomainService : DomainService, IEmpresaDomainService
    {
        protected readonly IEmpresaRepository _repository;

        public EmpresaDomainService(IEmpresaRepository repository, INotificationHandler notificationHandler) 
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public async virtual Task<bool> DeleteAsync(long Id) =>
           await _repository.DeleteAsync(Id);

        public async virtual Task<Empresa> FindByIdAsync(long Id) =>
            await _repository.FindByIdAsync(Id);


        
        public async virtual Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request) =>
            await _repository.GetAllAsync(request);

        public async virtual Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request)
        {
            return await _repository.GetAllWithFiltersAsync(request);
        }

        public async virtual Task<EmpresaDTO> GetAsync(DefaultRequestDto key) =>
            await _repository.GetAsync(key);

        public virtual async Task<Empresa> InsertAsync(Empresa.Builder builder)
        {
            if (builder == null)
            {
                Notification.RaiseError(Constants.LocalizationSourceName,
                    Error.DomainServiceOnUpdateNullBuilderError);
                return default;
            }

            var entity = BuildEntity(builder);

            if (Notification.HasNotification())
                return default;

            return await _repository.InsertAsync(entity);
        }

        public virtual async Task<Empresa> UpdateAsync(Empresa.Builder builder)
        {
            if (builder == null)
            {
                Notification.RaiseError(Constants.LocalizationSourceName,
                    Error.DomainServiceOnUpdateNullBuilderError);
                return default;
            }

            var entity = BuildEntity(builder);

            if (Notification.HasNotification())
                return default;

            return await _repository.UpdateAsync(entity);
        }

        protected Empresa BuildEntity(Empresa.Builder builder) =>
            builder.Build();
    }
}
