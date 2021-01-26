using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace FiscalPrime.Backend.Shared
{
    public static class Constants
    {
        public const string RptFolder = "Rpt";

        public enum Relatorios
        {
            logs = 1,
            empresas = 2
        }

        #region Domínios de Referências Externas - SPED
        public enum Dominio
        {
            [Description("Tipo do item – Atividades Industriais, Comerciais e Serviços")]
            TIPO_ITEM = 1,

            [Description("Código da Nomenclatura Comum do Mercosul")]
            COD_NCM = 2,

            [Description("Código Especificador da Substituição Tributária")]
            CEST = 3,

            [Description("Código do gênero do item, conforme a Tabela 4.2.1")]
            COD_GEN = 4,

            [Description("Código do serviço conforme lista do Anexo I da Lei Complementar Federal nº 116-03.")]
            COD_LST = 5,

            [Description("Código do país do participante, conforme a tabela indicada no item 3.2.1")]
            COD_PAIS = 6,

            [Description("UF Código IBGE - Sigla")]
            COD_UF = 7,

            [Description("Código do município do domicílio fiscal da entidade, conforme a tabela IBGE")]
            COD_MUN = 8,

            [Description("Indicador do tipo de operação")]
            IND_OPER = 9,

            [Description("Indicador do emitente do documento fiscal")]
            IND_EMIT = 10,

            [Description("Código da situação do documento conforme tabela 4.1.2")]
            COD_SIT = 11,

            [Description("Código do modelo do documento fiscal, conforme a Tabela 4.1.3")]
            COD_MOD = 12,

            [Description("Indicador do tipo de pagamento")]
            IND_PGTO = 13,

            [Description("Indicador do tipo do frete")]
            IND_FRT = 14,

            [Description("Indicador do tipo de operação")]
            OPER = 15,

            [Description("Indicador do tipo de título de crédito")]
            IND_TIT = 16,

            [Description("Código de classe de consumo de energia elétrica ou gás")]
            COD_CONS = 17,

            [Description("Código de classe de consumo de Fornecimento D´água – Tabela 4.4.2")]
            COD_CONS_AGUA = 18,

            [Description("Código de tipo de Ligação")]
            TP_LIGACAO = 19,

            [Description("Código de grupo de tensão")]
            COD_GRUPO_TENSAO = 20,

            [Description("Finalidade da emissão do documento eletrônico")]
            FIN_DOCE = 21,

            [Description("Indicador do Destinatário-Acessante")]
            IND_DEST = 22,

            [Description("Código do Tipo de Assinante")]
            TP_ASSINANTE = 23,

            [Description("Indicador de movimento")]
            IND_MOV = 24,

            [Description("Código da Situação Tributária referente ao ICMS, conforme a Tabela indicada no item 4.3.1")]
            CST_ICMS = 25,

            [Description("Código Fiscal de Operação e Prestação")]
            CFOP = 26,

            [Description("Indicador de período de apuração do IPI")]
            IND_APUR = 27,

            [Description("Código da Situação Tributária referente ao IPI, conforme a Tabela indicada no item 4.3.2.")]
            CST_IPI = 28,

            [Description("Código de enquadramento legal do IPI, conforme tabela indicada no item 4.5.3.")]
            COD_ENQ = 29,

            [Description("Código da Situação Tributária referente ao PIS.")]
            CST_PIS = 30,

            [Description("Código da Situação Tributária referente ao COFINS.")]
            CST_COFINS = 31,

            [Description("Código do selo de controle do IPI, conforme Tabela 4.5.2")]
            COD_SELO_IPI = 32,

            [Description("Código da classe de enquadramento do IPI, conforme Tabela 4.5.1.")]
            CL_ENQ = 33,

            [Description("Código que indica o responsável pela retenção do ICMS ST")]
            COD_RESP_RET = 34,

            [Description("Código do modelo do documento de arrecadação")]
            COD_DA = 35,

            [Description("Código de classificação do item de energia elétrica, conforme a Tabela 4.4.1")]
            COD_CLASS = 36,

            [Description("Indicador do tipo de receita")]
            IND_REC = 37
        }
        #endregion Domínios de Referências Externas - SPED
    }
}
