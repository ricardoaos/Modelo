using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Domain.Entities.DominioExterno;
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
    public class DominioExternoDomainService : DomainService, IDominioExternoDomainService
    {
        protected readonly IDominioExternoRepository _repository;

        public DominioExternoDomainService(IDominioExternoRepository repository, INotificationHandler notificationHandler) 
            : base(notificationHandler)
        {
            _repository = repository;
        }

        public async virtual Task<bool> DeleteAsync(long Id) =>
           await _repository.DeleteAsync(Id);

        public async virtual Task<DominioExterno> FindByIdAsync(long Id) =>
            await _repository.FindByIdAsync(Id);


        
        public async virtual Task<IListDto<DominioExternoDTO>> GetAllAsync(SearchRequestAllDTO request) =>
            await _repository.GetAllAsync(request);

        public async virtual Task<IListDto<DominioExternoDTO>> GetAllWithDomain(DominioExternoRequestAllDTO request)
        {
            return await _repository.GetAllWithDomain(request);
        }

        public async virtual Task<DominioExternoDTO> GetAsync(DefaultRequestDto key) =>
            await _repository.GetAsync(key);

        public virtual async Task<DominioExterno> InsertAsync(DominioExterno.Builder builder)
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

        public virtual async Task<DominioExterno> UpdateAsync(DominioExterno.Builder builder)
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

        protected DominioExterno BuildEntity(DominioExterno.Builder builder) =>
            builder.Build();
    }
}
