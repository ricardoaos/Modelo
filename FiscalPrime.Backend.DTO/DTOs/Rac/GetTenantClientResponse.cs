using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class GetTenantClientResponse
    {
        public string clientId { get; set; }
        public string clientName { get; set; }
        public int productId { get; set; }
        public string productName { get; set; }
        public string tenantId { get; set; }
        public string tenantName { get; set; }
        public SecretDto [] secrets { get; set; }
    }
}
