using FiscalPrime.Backend.Domain.Configurations;
using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace FiscalPrime.Backend.Domain.Entities.Empresas.Specifications
{

    public class CodigoMunicipioIBGECannotNullOrZeroSpecification : Specification<Empresa>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Empresa.Validations.CodigoMunicipioIBGECannotNullOrZero;

        public override Expression<Func<Empresa, bool>> ToExpression()
        {
            return (p) => (p.CodigoMunicipioIBGE > 0);
        }
    }    
}
