﻿using FiscalPrime.Backend.Domain.Configurations;
using System;
using System.Linq.Expressions;
using Tnf.Specifications;

namespace FiscalPrime.Backend.Domain.Entities.Empresas.Specifications
{

    public class CepCannotBeEmptySpecification : Specification<Empresa>
    {
        public override string LocalizationSource { get; protected set; } = Constants.LocalizationSourceName;
        public override Enum LocalizationKey { get; protected set; } = Empresa.Validations.CepCannotBeEmpty;

        public override Expression<Func<Empresa, bool>> ToExpression()
        {
            return (p) => !string.IsNullOrEmpty(p.Cep);
        }
    }   
}
