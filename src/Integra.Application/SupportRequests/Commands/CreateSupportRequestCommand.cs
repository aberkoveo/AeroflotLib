using MediatR;
using Integra.Domain.Support;

namespace Integra.Application.SupportRequests.Commands;

/// <summary>
/// Тип позволяет создавать записи в соотв. СУБД
/// </summary>
public class CreateSupportRequestCommand : IRequest<int>
{
    public int SMID { get; set; }
    public Priority Priority { get; set; }
    public string BatchId { get; set; }
    public string BatchOwner { get; set; }
    public string Categories { get; set; }
    public string Comment { get; set; }
    public string DocumentsIds { get; set; }
}