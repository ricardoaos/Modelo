using FiscalPrime.Backend.Domain.Entities.Empresas.Specifications;
using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Domain.Entities.Empresas
{
    public partial class Empresa
    {
        public class Builder : Builder<Empresa>
        {
            #region ctor
            public Builder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public Builder(INotificationHandler notificationHandler, Empresa instance)
                : base(notificationHandler, instance)
            {
            }
            #endregion

            #region with            

            public Builder WithId(long id)
            {
                Instance.Id = id;
                return this;
            }

            public Builder WithNumeroCNPJCPF(string numeroCNPJCPF)
            {
                Instance.NumeroCNPJCPF = numeroCNPJCPF;
                return this;
            }

            public Builder WithNome(string nome)
            {
                Instance.Nome = nome;
                return this;
            }

            public Builder WithNomeFantasia(string nomeFantasia)
            {
                Instance.NomeFantasia = nomeFantasia;
                return this;
            }

            public Builder WithEndereco(string endereco)
            {
                Instance.Endereco = endereco;
                return this;
            }

            public Builder WithNumero(string numero)
            {
                Instance.Numero = numero;
                return this;
            }

            public Builder WithComplemento(string complemento)
            {
                Instance.Complemento = complemento;
                return this;
            }

            public Builder WithBairro(string bairro)
            {
                Instance.Bairro = bairro;
                return this;
            }
            public Builder WithCodigoMunicipioIBGE(int codigoMunicipioIBGE)
            {
                Instance.CodigoMunicipioIBGE = codigoMunicipioIBGE;
                return this;
            }

            public Builder WithCodigoUF(short codigoUF)
            {
                Instance.CodigoUF = codigoUF;
                return this;
            }

            public Builder WithCep(string cep)
            {
                Instance.Cep = cep;
                return this;
            }

            public Builder WithCodigoPais(short codigoPais)
            {
                Instance.CodigoPais = codigoPais;
                return this;
            }

            public Builder WithNomePais(string nomePais)
            {
                Instance.NomePais = nomePais;
                return this;
            }

            public Builder WithTelefone(string telefone)
            {
                Instance.Telefone = telefone;
                return this;
            }

            public Builder WithInscricaoEstadual(string inscricaoEstadual)
            {
                Instance.InscricaoEstadual = inscricaoEstadual;
                return this;
            }

            public Builder WithInscricaoEstadualST(string inscricaoEstadualST)
            {
                Instance.InscricaoEstadualST = inscricaoEstadualST;
                return this;
            }

            public Builder WithInscricaoMunicipal(string inscricaoMunicipal)
            {
                Instance.InscricaoMunicipal = inscricaoMunicipal;
                return this;
            }

            public Builder WithCodigoCNAE(string codigoCNAE)
            {
                Instance.CodigoCNAE = codigoCNAE;
                return this;
            }

            public Builder WithCodigoCRT(int? codigoCRT)
            {
                Instance.CodigoCRT = codigoCRT;
                return this;
            }

            public Builder WithCodigoSuframa(string codigoSuframa)
            {
                Instance.CodigoSuframa = codigoSuframa;
                return this;
            }

            public Builder WithAtivo(bool ativo)
            {
                Instance.Ativo = ativo;
                return this;
            }

            public Builder WithDataInclusao(DateTime dataInclusao)
            {
                Instance.DataInclusao = dataInclusao;
                return this;
            }

            public Builder WithDataUltimaAlteracao(DateTime dataUltimaAlteracao)
            {
                Instance.DataUltimaAlteracao = dataUltimaAlteracao;
                return this;
            }            

            #endregion

            protected override void Specifications()
            {
                base.Specifications();

                AddSpecification<BairroCannotBeEmptySpecification>();
                AddSpecification<CepCannotBeEmptySpecification>();
                AddSpecification<CodigoMunicipioIBGECannotNullOrZeroSpecification>();
                AddSpecification<CodigoPaisCannotNullOrZeroSpecification>();
                AddSpecification<CodigoUFCannotNullOrZeroSpecification>();
                AddSpecification<EnderecoCannotBeEmptySpecification>();
                AddSpecification<NomeCannotBeEmptySpecification>();
                AddSpecification<NomeFantasiaCannotBeEmptySpecification>();
                AddSpecification<NomePaisCannotBeEmptySpecification>();
                AddSpecification<NumeroCannotBeEmptySpecification>();
                AddSpecification<NumeroCNPJCPFCannotNullOrZeroSpecification>();
                AddSpecification<TelefoneCannotBeEmptySpecification>();

            }
        }
    }
}
