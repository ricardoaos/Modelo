using System;
using System.Collections.Generic;
using System.Text;

namespace FiscalPrime.Backend.DTO.DTOs.Rac
{
    public class UserDTO
    {
        public Int64 id { get; set; }
        public string userName { get; set; }
        public string emailAddress { get; set; }
        public string fullName { get; set; }
        public bool readOnly { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phoneNumber { get; set; }
        public Nullable<bool> isUndefinedPassword { get; set; }
        public Nullable<bool> sentEmailSuccessfully { get; set; }
        public bool isActive { get; set; }
        public string externalId { get; set; }
        public Int64[] organizations { get; set; }
        public Int32[] roles { get; set; }
        public Int64[] productRoles { get; set; }
        public string[] _expandables { get; set; }
    }
}
