using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.DTOs.Rac;
using FiscalPrime.Backend.Shared;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiscalPrime.Backend.HangFire.Tasks
{
    public class TaskSchedule
    {
        private int page = 1;
        private int pageSize = 60;

        private static string racUrl;
        private static string clientIdMaster;
        private static string clientSecretMaster;
        private static string userNameRac;
        private static string passwordUserRac;

        private static string clientIdFiscalPrime;
        private static string clientSecretFiscalPrime;
        private static string userNameGeneric;
        private static string passwordUserGeneric;

        private static string racGetAllTenantsUrl;
        private static string racGetOrganizationUnitUrl;
        private static string racGetTenantByIdUrl;
        private static string racGetOrganizationUnitByIdUrl;

        private static string fiscalPrimeEmpresasUrl;
        private static string fiscalPrimeEmpresasByIdUrl;

        private void FillConfiguration(string configuration)
        {
            Dictionary<string, string> dicParametros = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configuration);
            
            racUrl = dicParametros["RacUrl"];
            clientIdMaster = dicParametros["ClientIdMaster"];
            clientSecretMaster = dicParametros["ClientSecretMaster"];
            userNameRac = dicParametros["UserNameRac"];
            passwordUserRac = dicParametros["PasswordUserRac"];

            clientIdFiscalPrime = dicParametros["ClientIdFiscalPrime"];
            clientSecretFiscalPrime = dicParametros["ClientSecretFiscalPrime"];
            userNameGeneric = dicParametros["UserNameGeneric"];
            passwordUserGeneric = dicParametros["PasswordUserGeneric"];

            racGetAllTenantsUrl = dicParametros["RacGetAllTenantsUrl"];
            racGetTenantByIdUrl = dicParametros["RacGetTenantByIdUrl"];
            racGetOrganizationUnitUrl = dicParametros["RacGetOrganizationUnitUrl"];
            racGetOrganizationUnitByIdUrl = dicParametros["RacGetOrganizationUnitByIdUrl"];
            fiscalPrimeEmpresasUrl = dicParametros["FiscalPrimeEmpresasUrl"];
            fiscalPrimeEmpresasByIdUrl = dicParametros["FiscalPrimeEmpresasByIdUrl"];
        }


        public async Task ClientsSinc(string configuration)
        {
            FillConfiguration(configuration);

            IList<TenantDto> lstTenantFiscalPrime = await GetAllFiscalPrimeTenants();

            if (lstTenantFiscalPrime.Count > 0)
            {
                Task insertUpdateCompanies = Task.Run(() =>
                {
                    InsertUpdateCompanies(lstTenantFiscalPrime);
                });

                insertUpdateCompanies.Wait();
            }
        }

        private async Task<IList<TenantDto>> GetAllFiscalPrimeTenants()
        {
            page = 1;

            var token = await Http.RacUtils.TokenUtils.GetResourceOwnerToken(racUrl, clientIdMaster, clientSecretMaster, userNameRac, passwordUserRac);

            IListDto<GetTenantsResponseDto> tenants = await Http.HttpUtils.HttpClientUtils<IListDto<GetTenantsResponseDto>>.GetTAsync(string.Format(racGetAllTenantsUrl, page, pageSize), token);

            List<TenantDto> lstTenantFiscalPrime = new List<TenantDto>();
            while (tenants.hasNext)
            {
                page += 1;

                tenants = await Http.HttpUtils.HttpClientUtils<IListDto<GetTenantsResponseDto>>.GetTAsync(string.Format(racGetAllTenantsUrl, page, pageSize), token);

                for (int i = 0; i < tenants.items.Count(); i++)
                {
                    TenantDto tenant = await Http.HttpUtils.HttpClientUtils<TenantDto>.GetTAsync(string.Format(racGetTenantByIdUrl, tenants.items[i].id), token);

                    bool isFiscalPrime = false;
                    if (tenant.products != null && tenant.products.Count() > 0)
                        isFiscalPrime = tenant.products.Where(p => p.productName.ToLower() == "fiscal prime").Count() > 0;

                    if (isFiscalPrime)
                        lstTenantFiscalPrime.Add(tenant);
                }
            }

            return lstTenantFiscalPrime;
        }

        private async void InsertUpdateCompanies(IList<TenantDto> lstTenantDto)
        {
            for (int i = 0; i < lstTenantDto.Count; i++)
            {
                page = 1;

                List<OrganizationUnitDto> organizationUnitsResponse = new List<OrganizationUnitDto>();
                try
                {

                    var token = await Http.RacUtils.TokenUtils.GetResourceOwnerToken(string.Format("https://{0}.rac.dev.totvs.io/totvs.rac", lstTenantDto[i].tenantName), clientIdFiscalPrime, clientSecretFiscalPrime, userNameGeneric, passwordUserGeneric);

                    IListDto<OrganizationUnitDto> lstOrganizationUnit = await Http.HttpUtils.HttpClientUtils<IListDto<OrganizationUnitDto>>.GetTAsync(string.Format(racGetOrganizationUnitUrl, lstTenantDto[i].tenantName, page, pageSize), token);

                    if (lstOrganizationUnit.items != null && lstOrganizationUnit.items.Count() > 0)
                    {
                        organizationUnitsResponse.AddRange(lstOrganizationUnit.items);

                        while (lstOrganizationUnit.hasNext)
                        {
                            page += 1;

                            lstOrganizationUnit = await Http.HttpUtils.HttpClientUtils<IListDto<OrganizationUnitDto>>.GetTAsync(string.Format(racGetOrganizationUnitUrl, lstTenantDto[i].tenantName, page, pageSize), token);
                            organizationUnitsResponse.AddRange(lstOrganizationUnit.items);
                        }

                        for (int x = 0; x < organizationUnitsResponse.Count; x++)
                        {
                            OrganizationUnitDto organizationUnit = await Http.HttpUtils.HttpClientUtils<OrganizationUnitDto>.GetTAsync(string.Format(racGetOrganizationUnitByIdUrl, lstTenantDto[i].tenantName, organizationUnitsResponse[x].id), token);

                            organizationUnitsResponse[x].parentId = organizationUnit.parentId;
                        }

                        organizationUnitsResponse = organizationUnitsResponse.OrderBy(o => o.parentId).Where(o => !string.IsNullOrEmpty(o.cnpj)).ToList();

                        page = 1;
                        List<EmpresaDTO> empresasFiscalPrimeResponse = new List<EmpresaDTO>();

                        IListDto<EmpresaDTO> empresasFiscalPrime = await Http.HttpUtils.HttpClientUtils<IListDto<EmpresaDTO>>.GetTAsync(string.Format(fiscalPrimeEmpresasUrl, page, pageSize), token);

                        if (empresasFiscalPrime.items != null && empresasFiscalPrime.items.Count() > 0)
                        {
                            empresasFiscalPrimeResponse.AddRange(empresasFiscalPrime.items);
                            while (lstOrganizationUnit.hasNext)
                            {
                                page += 1;

                                empresasFiscalPrime = await Http.HttpUtils.HttpClientUtils<IListDto<EmpresaDTO>>.GetTAsync(string.Format(fiscalPrimeEmpresasUrl, page, pageSize), token);
                                empresasFiscalPrimeResponse.AddRange(empresasFiscalPrime.items);
                            }
                        }

                        for (int x = 0; x < organizationUnitsResponse.Count; x++)
                        {
                            int qtdCompaniesFound = empresasFiscalPrimeResponse.Where(e => e.NumeroCNPJCPF == organizationUnitsResponse[x].cnpj).Count();

                            if (qtdCompaniesFound <= 1)
                            {
                                EmpresaDTO empresa = empresasFiscalPrimeResponse.Where(e => e.NumeroCNPJCPF == organizationUnitsResponse[x].cnpj).FirstOrDefault();

                                if (empresa is null)
                                {
                                    //Adiciona empresa
                                    EmpresaDTO empresaAdicionar = new EmpresaDTO()
                                    {
                                        NumeroCNPJCPF = organizationUnitsResponse[x].cnpj,
                                        Nome = organizationUnitsResponse[x].name,
                                        Telefone = "00000000",
                                        Endereco = "xx",
                                        CodigoUF = 1,
                                        CodigoMunicipioIBGE = 1,
                                        NomeFantasia = organizationUnitsResponse[x].name,
                                        CodigoPais = 55,
                                        NomePais = "Brazil",
                                        Bairro = "xx",
                                        Cep = "00000000",
                                        Numero = "123"
                                    };

                                    empresaAdicionar = await Http.HttpUtils.HttpClientUtils<EmpresaDTO>.PostTAsync(fiscalPrimeEmpresasUrl, empresaAdicionar, token);
                                }
                                else
                                {
                                    EmpresaDTO empresaAtualizar = new EmpresaDTO()
                                    {
                                        Id = empresa.Id,
                                        NumeroCNPJCPF = empresa.NumeroCNPJCPF,
                                        Nome = organizationUnitsResponse[x].name,
                                        Telefone = empresa.Telefone,
                                        Endereco = empresa.Endereco,
                                        CodigoUF = empresa.CodigoUF,
                                        CodigoMunicipioIBGE = empresa.CodigoMunicipioIBGE,
                                        NomeFantasia = organizationUnitsResponse[x].name,
                                        CodigoPais = empresa.CodigoPais,
                                        NomePais = empresa.NomePais,
                                        Bairro = empresa.Bairro,
                                        Cep = empresa.Cep,
                                        Numero = empresa.Numero,
                                        DataInclusao = empresa.DataInclusao,
                                        DataUltimaAlteracao = empresa.DataUltimaAlteracao,
                                        Expandables = empresa.Expandables
                                    };

                                    bool existFieldsChanged = empresaAtualizar.DetailedCompare(empresa).Count() > 0;

                                    if (existFieldsChanged)
                                        empresaAtualizar = await Http.HttpUtils.HttpClientUtils<EmpresaDTO>.PutTAsync(string.Format(fiscalPrimeEmpresasByIdUrl, empresaAtualizar.Id), empresaAtualizar, token);

                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }
    }
}
