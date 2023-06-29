using AutoMapper;
using Integra.Domain;
using Integra.Application.Common.Mappings;
using Integra.Application.SupportRequests.Commands;

namespace Integra.WebApi.Models;

public class CreateSupportRequestDto : IMapWith<CreateSupportRequestCommand>
{
    public int ID { get; set; }
    public int SMID { get; set; }
    public Priority Priority { get; set; }
    public string BatchId { get; set; }
    public string BatchOwner { get; set; }
    public string Categories { get; set; }
    public string Comment { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<CreateSupportRequestDto, CreateSupportRequestCommand>()
            .ForMember(command => command.SMID, opt => opt.MapFrom(dto => dto.SMID))
            .ForMember(command => command.Priority, opt => opt.MapFrom(dto => dto.Priority))
            .ForMember(command => command.BatchId, opt => opt.MapFrom(dto => dto.BatchId))
            .ForMember(command => command.BatchOwner, opt => opt.MapFrom(dto => dto.BatchOwner))
            .ForMember(command => command.Categories, opt => opt.MapFrom(dto => dto.Categories))
            .ForMember(command => command.Comment, opt => opt.MapFrom(dto => dto.Comment));
    }
}