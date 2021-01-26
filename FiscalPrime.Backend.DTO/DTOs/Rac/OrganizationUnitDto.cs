using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class OrganizationUnitDto
    {
        public Int64 id { get; set; }
        public string name { get; set; }
        public Int64 parentId { get; set; }
        public string cnpj { get; set; }
        public string[] _expandables { get; set; }
    }
}
