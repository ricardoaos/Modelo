using FiscalPrime.Backend.Domain.Interfaces;
using System;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Domain.Entities.DominioExterno
{
    public partial class DominioExterno : IEntity
    {
        public long Id { get; set; }
        public long IdDominio { get; set; }

        public string UF { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }

        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFimVigencia { get; set; }

        public bool Ativo { get; set; }
        public DateTime DataInclusao { get; set; }
        public DateTime DataUltimaAlteracao { get; set; }

        public static Builder Create(INotificationHandler handler)
        => new Builder(handler);

        public static Builder Create(INotificationHandler handler, DominioExterno instance)
        => new Builder(handler, instance);
    }
}
