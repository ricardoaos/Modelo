using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class GetTenantsResponseDto
    {
        public string id { get; set; }
        public string tenantName { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
    }
}
