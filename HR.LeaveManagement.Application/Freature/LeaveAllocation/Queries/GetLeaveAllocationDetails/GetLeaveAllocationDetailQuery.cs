using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailQuery : IRequest<LeaveAllocationDetailsDto>
{
    public GetLeaveAllocationDetailQuery(int id)
    {
        Id = id;
    }
    public int Id { get; set; }
}
