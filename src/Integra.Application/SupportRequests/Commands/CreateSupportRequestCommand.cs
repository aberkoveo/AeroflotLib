using MediatR;
using Integra.Domain;

namespace Integra.Application.SupportRequests.Commands;

public class CreateSupportRequestCommand : IRequest<int>
{
    public int ID { get; set; }
    public int SMID { get; set; }
    public string CreationDate { get; set; }
    public Priority Priority { get; set; }
    public string BatchId { get; set; }
    public string BatchOwner { get; set; }
    public string Categories { get; set; }
    public string Comment { get; set; }
}