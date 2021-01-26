using FiscalPrime.Backend.Domain.Interfaces.DomainServices;
using FiscalPrime.Backend.Domain.Interfaces.Repositories;
using FiscalPrime.Backend.Domain.Configurations;
using FiscalPrime.Backend.Domain.DomainServices;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using Tnf.TestBase;
using Xunit;
using System.Threading.Tasks;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Tests.Entidades.Mocks;

namespace FiscalPrime.Backend.Tests
{
    public class EmpresaTest : TnfIntegratedTestBase
    {
        #region "Variaveis e Métodos internos"

        private readonly IEmpresaDomainService _domainService;
        private readonly IEmpresaRepository _repository;
        private readonly CultureInfo _culture;

        public EmpresaTest()
        {
            _domainService = Resolve<IEmpresaDomainService>();
            _repository = Resolve<IEmpresaRepository>();

            _culture = CultureInfo.GetCultureInfo("pt-BR");
        }

        private Empresa.Builder CriarEmpresa(int i)
        {
            return Empresa.Create(LocalNotification)
                        .WithBairro($"Bairro")
                        .WithCep("06286060")
                        .WithCodigoMunicipioIBGE(1)
                        .WithCodigoUF(1)
                        .WithCodigoPais(1)
                        .WithEndereco($"Endereco {i}")
                        .WithNome($"Nome {i}")
                        .WithNomeFantasia($"Nome Fantasia {i}")
                        .WithNomePais($"Nome Pais {i}")
                        .WithNumero(i.ToString())
                        .WithNumeroCNPJCPF(i.ToString())
                        .WithTelefone(i.ToString());
        }


        protected override void PreInitialize(IServiceCollection services)
        {
            base.PreInitialize(services);

            // Registro dos serviços de Mock
            services.AddTransient<IEmpresaRepository, EmpresaRepositoryMock>();

            // Registro dos serviços para teste
            services.AddTransient<IEmpresaDomainService, EmpresaDomainService>();

            services.ConfigureTnf(builder => builder.UseDomainLocalization());
        }

        #endregion

        #region "DI"        

        [Fact]
        [Trait("DI", "")]
        public void Deve_Resolver_Todas_Dependencias()
        {
            Assert.NotNull(TnfSession);
            Assert.NotNull(ServiceProvider.GetService<IEmpresaDomainService>());
            Assert.NotNull(ServiceProvider.GetService<IEmpresaRepository>());
        }
        #endregion

        #region "Insert"

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Se_Criado_Com_Null()
        {
            // Act
            var product = await _domainService.InsertAsync(null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            Assert.Null(product);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Bairro_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                                                            .WithBairro(string.Empty));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.BairroCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_CEP_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                .WithCep(string.Empty));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CepCannotBeEmpty, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_CodigoMunicipioIBGE()
        {

            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                .WithCodigoMunicipioIBGE(0));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoMunicipioIBGECannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_CodigoPais_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                .WithCodigoPais(0));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoPaisCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_CodigoUF_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                .WithCodigoUF(0));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoUFCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Endereco_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                 .WithEndereco(string.Empty));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.EnderecoCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Nome_Vazio()
        {
            var gravado = await _domainService.InsertAsync(CriarEmpresa(1)
                .WithNome(string.Empty));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomeCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_NomeFantasia_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNomeFantasia(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomeFantasiaCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_NomePais_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNomePais(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomePaisCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Numero_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNumero(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NumeroCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_NumeroCNPJCPF_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNumeroCNPJCPF(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NumeroCNPJCPFCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Insert", "Specification")]
        public async Task Deve_Notificar_Inserir_Telefone_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithTelefone(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.TelefoneCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        #endregion

        #region "Update"

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Se_Criado_Com_Null()
        {
            // Act
            var product = await _domainService.UpdateAsync(null);

            // Assert
            Assert.True(LocalNotification.HasNotification());
            Assert.Null(product);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Bairro_Vazio()
        {
            var gravado = await _domainService.UpdateAsync(CriarEmpresa(1)
                .WithBairro(string.Empty));

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.BairroCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);

        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_CEP_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithCep(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CepCannotBeEmpty, _culture);
            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_CodigoMunicipioIBGE()
        {
            var empresa = CriarEmpresa(1)
                .WithCodigoMunicipioIBGE(0);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoMunicipioIBGECannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task DDeve_Notificar_Update_CodigoPais_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithCodigoPais(0);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoPaisCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_CodigoUF_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithCodigoUF(0);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.CodigoUFCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Endereco_Vazio()
        {
            var empresa = CriarEmpresa(1)
                 .WithEndereco(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.EnderecoCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Nome_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNome(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomeCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_NomeFantasia_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNomeFantasia(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomeFantasiaCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_NomePais_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNomePais(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NomePaisCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Numero_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNumero(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NumeroCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_NumeroCNPJCPF_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithNumeroCNPJCPF(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.NumeroCNPJCPFCannotNullOrZero, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        [Fact]
        [Trait("Update", "Specification")]
        public async Task Deve_Notificar_Update_Telefone_Vazio()
        {
            var empresa = CriarEmpresa(1)
                .WithTelefone(string.Empty);

            var gravado = await _domainService.InsertAsync(empresa);

            var message = GetLocalizedString(Constants.LocalizationSourceName,
                                             Empresa.Validations.TelefoneCannotBeEmpty, _culture);

            Assert.Contains(LocalNotification.GetAll(), n => n.Message == message);
        }

        #endregion

        #region Delete
        [Fact]
        [Trait("Delete", "")]
        public Task Deve_Excluir()
        {
            return _domainService.DeleteAsync(1);
        }

        #endregion

        #region FindById
        [Fact]
        [Trait("Get", "FindById")]
        public Task Deve_Buscar_Por_Id()
        {
            return _domainService.FindByIdAsync(1);
        }

        #endregion

        #region Get
        [Fact]
        [Trait("Get", "Find By Key")]
        public Task Deve_Buscar_Por_Key()
        {
            return _domainService.GetAsync(new DTO.Requests.DefaultRequestDto { Id = 1 });
        }

        #endregion

        #region GetAll
        [Fact]
        [Trait("Get", "Find By Key")]
        public Task Deve_Buscar_Todos()
        {
            return _domainService.GetAllAsync(new DTO.Requests.RequestAll.SearchRequestAllDTO());
        }

        #endregion

        #region GetAll
        [Fact]
        [Trait("Get", "Find By Filters")]
        public Task Deve_Buscar_Todos_ComFiltro()
        {
            return _domainService.GetAllAsync(new DTO.Requests.RequestAll.EmpresaRequestAllDTO { Ativo = "true, false", Nome = "empresa" });
        }

        #endregion

    }
}
