using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class TenantDto
    {
        public string id { get; set; }
public string tenantName { get; set; }
        public string emailAddress { get; set; }
        public string adminUserEmailAddress { get; set; }
        public string name { get; set; }
        public string cpf { get; set; }
        public string cnpj { get; set; }
        public bool isActive { get; set; }
        public bool isUndefinedPasswordForAdminUser { get; set; }
        public Nullable<bool> sentEmailSuccessfully { get; set; }
        public TenantProductDto [] products { get; set; }
    }
}
