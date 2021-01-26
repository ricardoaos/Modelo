using System;
using Tnf.Builder;
using Tnf.Notifications;

namespace FiscalPrime.Backend.Domain.Entities.DominioExterno
{
    public partial class DominioExterno
    {
        public class Builder : Builder<DominioExterno>
        {
            #region ctor
            public Builder(INotificationHandler notificationHandler)
                : base(notificationHandler)
            {
            }

            public Builder(INotificationHandler notificationHandler, DominioExterno instance)
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

            public Builder WithIdDominio(long idDominio)
            {
                Instance.IdDominio = idDominio;
                return this;
            }

            public Builder WithUF(string uf)
            {
                Instance.UF = uf;
                return this;
            }

            public Builder WithCodigo(string codigo)
            {
                Instance.Codigo = codigo;
                return this;
            }

            public Builder WithDescricao(string descricao)
            {
                Instance.Descricao = descricao;
                return this;
            }

            public Builder WithDataInicioVigencia(DateTime dataInicioVigencia)
            {
                Instance.DataInicioVigencia = dataInicioVigencia;
                return this;
            }

            public Builder WithDataFimVigencia(DateTime dataFimVigencia)
            {
                Instance.DataFimVigencia = dataFimVigencia;
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



            }
        }
    }
}
