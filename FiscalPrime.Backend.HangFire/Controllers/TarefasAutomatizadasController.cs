using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Tnf.AspNetCore.Mvc.Response;
using Tnf.Runtime.Session;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using IdentityModel.Client;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using FiscalPrime.Backend.DTO.DTOs.Rac;
using Hangfire;
using Hangfire.Common;
using FiscalPrime.Backend.HangFire.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace FiscalPrime.Backend.HangFire.Controllers
{
    [Route(WebConstants.TarefasAutomatizadasRouteName)]
    public class TarefasAutomatizadasController : TnfController
    {
        private const string _name = "TarefasAutomatizadas";

        private readonly ITnfSession _tnfSession;

        private readonly Dictionary<string, string> _automatizadorConfig;

        public TarefasAutomatizadasController(ITnfSession tnfSession, Dictionary<string, string> automatizadorConfig)
        {
            _tnfSession = tnfSession;

            this._automatizadorConfig = automatizadorConfig;
        }

        /// <summary>
        /// Método que inicia o agendamento das tarefas
        /// </summary>
        /// <returns>Retorna view com o relatório</returns>
        [HttpGet("IniciarAgendamentoDeTarefas")]
        [ProducesResponseType(typeof(IActionResult), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> IniciarAgendamentoDeTarefas()
        {
            var manager = new RecurringJobManager();

            //Inicia tarefas de sincronização de clientes
            manager.AddOrUpdate("ClientsSinc", Job.FromExpression(() => new TaskSchedule().ClientsSinc(Newtonsoft.Json.JsonConvert.SerializeObject(_automatizadorConfig))), Cron.MinuteInterval(Convert.ToInt32(_automatizadorConfig["TempoAtualizacaoAutomaticaClientes"])));

            return CreateResponseOnGet("Agendamento de tarefas executado com sucesso!");
        }
    }
}
