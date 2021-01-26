using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class TenantProductDto
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public bool canRemove { get; set; }
    }
}
