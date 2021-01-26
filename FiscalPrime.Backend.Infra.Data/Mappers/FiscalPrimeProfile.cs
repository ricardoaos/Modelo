using AutoMapper;
using FiscalPrime.Backend.Domain.Entities.DominioExterno;
using FiscalPrime.Backend.Domain.Entities.Empresas;
using FiscalPrime.Backend.DTO.DTOs;
using System;

namespace FiscalPrime.Backend.Infra.Data.Mappers
{
    public class FiscalPrimeProfile : Profile
    {
        public FiscalPrimeProfile()
        {
            CreateMap<Empresa, EmpresaDTO>();
            CreateMap<EmpresaDTO, Empresa>();
            CreateMap<DominioExterno, DominioExternoDTO>();
        }
    }
}
