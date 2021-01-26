using System;
using Tnf.Dto;

namespace FiscalPrime.Backend.DTO.Requests
{
    public interface IDefaultRequestDto : IRequestDto
    {
        long Id { get; set; }
    }
}
