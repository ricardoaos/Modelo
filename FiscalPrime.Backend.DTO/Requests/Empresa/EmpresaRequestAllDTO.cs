using Tnf.Dto;

namespace FiscalPrime.Backend.DTO.Requests.RequestAll
{
    public class EmpresaRequestAllDTO : SearchRequestAllDTO
    {
        public string Nome { get; set; }
        public string Ativo { get; set; }
        public int? CodigoMunicipioIBGE { get; set; }
    }
}
