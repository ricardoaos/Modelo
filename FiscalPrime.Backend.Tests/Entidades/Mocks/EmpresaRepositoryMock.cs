using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.Domain.Interfaces.Repositories;
using FiscalPrime.Backend.DTO.DTOs;
using FiscalPrime.Backend.DTO.Requests;
using FiscalPrime.Backend.DTO.Requests.RequestAll;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tnf.Dto;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Tests.Entidades.Mocks
{
    public class EmpresaRepositoryMock : IEmpresaRepository
    {
        private readonly INotificationHandler notificationHandler;
        private List<Empresa> list = new List<Empresa>();

        private void PreencherListaEmpresas()
        {
            for (int i = 1; i < 5; i++)
            {
                var empresa = CriarEmpresa(i);
                list.Add(empresa);
            }
        }
        public Empresa CriarEmpresa(int i) =>
            Empresa.Create(notificationHandler)
                        .WithId(i)
                        .WithBairro($"Bairro{i}")
                        .WithCodigoPais(short.Parse(i.ToString()))
                        .WithCep(i.ToString())
                        .WithCodigoMunicipioIBGE(1)
                        .WithCodigoUF(1)
                        .WithEndereco($"Endereco {i}")
                        .WithNome($"Nome {i}")
                        .WithNomeFantasia($"Nome Fantasia {i}")
                        .WithNomePais($"Nome Pais {i}")
                        .WithNumero(i.ToString())
                        .WithNumeroCNPJCPF(i.ToString())
                        .WithTelefone(i.ToString())
                        .Build();

        public EmpresaRepositoryMock(INotificationHandler notificationHandler)
        {
            this.notificationHandler = notificationHandler;
        }

        public Task<bool> DeleteAsync(long Id)
        {
            PreencherListaEmpresas();
            list.RemoveAll(c => c.Id == Id);
            return true.AsTask();
        }

        public Task<Empresa> FindByIdAsync(long Id)
        {
            PreencherListaEmpresas();
            return list.Find(x => x.Id == Id).AsTask();
        }

        public Task<IListDto<EmpresaDTO>> GetAllAsync(SearchRequestAllDTO request)
        {
            PreencherListaEmpresas();
            IListDto<EmpresaDTO> lista = new ListDto<EmpresaDTO>();
            foreach (Empresa item in list)
            {
                lista.Items.Add(new EmpresaDTO
                {
                    Id = item.Id,
                    Ativo = item.Ativo,
                    Bairro = item.Bairro,
                    Cep = item.Cep
                });
            }

            return lista.AsTask();
        }

        public Task<IListDto<EmpresaDTO>> GetAllWithFiltersAsync(EmpresaRequestAllDTO request)
        {
            PreencherListaEmpresas();
            IListDto<EmpresaDTO> lista = new ListDto<EmpresaDTO>();
            foreach (Empresa item in list)
            {
                lista.Items.Add(new EmpresaDTO
                {
                    Id = item.Id,
                    Ativo = item.Ativo,
                    Bairro = item.Bairro,
                    Cep = item.Cep
                });
            }

            return lista.AsTask();
        }

        public Task<EmpresaDTO> GetAsync(DefaultRequestDto key)
        {
            PreencherListaEmpresas();
            var item = list.Find(x => x.Id == key.Id);
            return new EmpresaDTO
            {
                Id = item.Id,
                Ativo = item.Ativo,
                Bairro = item.Bairro,
                Cep = item.Cep
            }.AsTask();
        }

        public Task<Empresa> InsertAsync(Empresa entity)
        {
            entity.Id = list.Count + 1;
            list.Add(entity);

            return entity.AsTask();
        }

        public Task<Empresa> UpdateAsync(Empresa entity)
        {
            list.RemoveAll(c => c.Id == entity.Id);
            list.Add(entity);

            return entity.AsTask();
        }
    }
}
