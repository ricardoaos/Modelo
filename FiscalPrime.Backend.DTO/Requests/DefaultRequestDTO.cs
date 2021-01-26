using Tnf.Dto;

namespace FiscalPrime.Backend.DTO.Requests
{
    public class DefaultRequestDto : RequestDto, IDefaultRequestDto
    {
        public DefaultRequestDto()
        {
        }

        public DefaultRequestDto(long id, RequestDto request)
        {
            Id = id;
            Fields = request.Fields;
            Expand = request.Expand;
        }

        public DefaultRequestDto(long id)
        {
            Id = id;
        }

        public long Id { get; set; }

    }
}
