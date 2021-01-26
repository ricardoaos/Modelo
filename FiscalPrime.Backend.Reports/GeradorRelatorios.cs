using FastReport.Data;
using FastReport.Web;
using FiscalPrime.Backend.DTO.DTOs.Reports;
using FiscalPrime.Backend.Shared;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;

namespace FiscalPrime.Backend.Reports
{
    public class UFCFOP
    {
        public List<UF> Ufs { get; set; }
    }

    public class UF
    {
        public string Sigla { get; set; }
        public string Nome { get; set; }

        public List<CFOP> Cfops { get; set; }
    }

    public class CFOP
    {
        public string SiglaCFOP { get; set; }
        public decimal Valor1 { get; set; }
        public decimal Valor2 { get; set; }

    }

    public static class GeradorRelatorios
    {
        public static WebReport GerarRelatorio(string report, Filters filtros)
        {
            WebReport webReport = new WebReport();

            switch (report.ToLower())
            {
                case nameof(Constants.Relatorios.logs):
                    {
                        webReport = GerarRelatorioLogs(filtros);
                        break;
                    }
                case nameof(Constants.Relatorios.empresas):
                    {
                        webReport = GerarRelatorioEmpresas(filtros);
                        break;
                    }
                default:
                    break;
            }

            return webReport;
        }

        private static WebReport GerarRelatorioLogs(Filters filtros)
        {

            FastReport.Web.WebReport webReport = new FastReport.Web.WebReport();

            var cfop1 = new CFOP
            {
                SiglaCFOP = "5101",
                Valor1 = 5101.01M,
                Valor2 = 5101.02M,
            };

            var cfop2 = new CFOP
            {
                SiglaCFOP = "5102",
                Valor1 = 5102.01M,
                Valor2 = 5102.02M,
            };

            var cfop3 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop4 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop5 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop6 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop7 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop8 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var cfop9 = new CFOP
            {
                SiglaCFOP = "5103",
                Valor1 = 5103.01M,
                Valor2 = 5103.02M,
            };

            var listacfop = new List<CFOP>();
            listacfop.Add(cfop1);
            listacfop.Add(cfop2);
            listacfop.Add(cfop3);
            listacfop.Add(cfop4);
            listacfop.Add(cfop5);
            listacfop.Add(cfop6);
            listacfop.Add(cfop7);
            listacfop.Add(cfop8);
            listacfop.Add(cfop9);

            var sp = new UF
            {
                Cfops = listacfop,
                Sigla = "SP",
                Nome = "São Paulo"
            };

            var rj = new UF
            {
                Cfops = listacfop,
                Sigla = "RJ",
                Nome = "Rio de Janeiro"
            };

            var sc = new UF
            {
                Cfops = listacfop,
                Sigla = "SC",
                Nome = "Santa Catarina"
            };

            var listaufcfop = new List<UF>();
            listaufcfop.Add(sp);
            listaufcfop.Add(rj);
            listaufcfop.Add(sc);


            var ufs = new UFCFOP
            {
                Ufs = listaufcfop
            };

            webReport.Report.Load(string.Format("{0}/{1}/{2}", Environment.CurrentDirectory, Constants.RptFolder, "teste.frx"));
            webReport.Report.RegisterData(listaufcfop, "CFOPxUF");
            webReport.Report.Prepare();

            return webReport;
        }

        private static WebReport GerarRelatorioEmpresas(Filters filtros)
        {

            FastReport.Web.WebReport webReport = new FastReport.Web.WebReport();

            FastReport.Utils.RegisteredObjects.AddConnection(typeof(PostgresDataConnection));
            webReport.Report.Load(string.Format("{0}/{1}/{2}", Environment.CurrentDirectory, Constants.RptFolder, "empresas.frx"));
            webReport.Report.SetParameterValue("NumeroCnpjCpfFilter", string.IsNullOrEmpty(filtros.CnpjCpf) ? "" : filtros.CnpjCpf);
            webReport.Report.Refresh();
            return webReport;
        }
    }
}
