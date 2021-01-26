using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.Domain.Interfaces;
using System;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Domain.Entities.Empresas
{
    public partial class Empresa : IEntity
    {
        public long Id { get; set; }
        public string NumeroCNPJCPF { get; set; }
        public string Nome { get; set; }
        public string NomeFantasia { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public int CodigoMunicipioIBGE { get; set; }
        public short CodigoUF { get; set; }
        public string Cep { get; set; }
        public short CodigoPais { get; set; }
        public string NomePais { get; set; }
        public string Telefone { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoEstadualST { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string CodigoCNAE { get; set; }
        public int? CodigoCRT { get; set; }
        public string CodigoSuframa { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public static Builder Create(INotificationHandler handler)
        => new Builder(handler);

        public static Builder Create(INotificationHandler handler, Empresa instance)
        => new Builder(handler, instance);
    }
}
