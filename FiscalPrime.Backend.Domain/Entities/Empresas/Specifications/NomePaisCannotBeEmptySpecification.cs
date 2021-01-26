using FiscalPrime.Backend.Domain.Configurations;
using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace FiscalPrime.Backend.Domain.Entities.Empresas.Specifications
{

    public class NomePaisCannotBeEmptySpecification : Specification<Empresa>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Empresa.Validations.NomePaisCannotBeEmpty;

        public override Expression<Func<Empresa, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrEmpty(p.NomePais);
        }
    }    
}
