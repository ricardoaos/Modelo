namespace FiscalPrime.Backend.Domain.Entities.Empresas
{
    public partial class Empresa
    {
        public enum Validations
        {
            NumeroCNPJCPFCannotNullOrZero,
            NomeCannotBeEmpty,
            NomeFantasiaCannotBeEmpty,
            EnderecoCannotBeEmpty,
            NumeroCannotBeEmpty,
            BairroCannotBeEmpty,
            CodigoMunicipioIBGECannotNullOrZero,
            CodigoUFCannotNullOrZero,
            CepCannotBeEmpty,
            CodigoPaisCannotNullOrZero,
            NomePaisCannotBeEmpty,
            TelefoneCannotBeEmpty                      
        }
    }
}
