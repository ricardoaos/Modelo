using FiscalPrime.Backend.Shared;
using System;
using Tnf.Dto;

namespace FiscalPrime.Backend.DTO.Requests.RequestAll
{
    public class DominioExternoRequestAllDTO : SearchRequestAllDTO
    {
        public Constants.Dominio IdDominio { get; set; }
        public string UF { get; set; }
        public string Codigo { get; set; }
    }
}
