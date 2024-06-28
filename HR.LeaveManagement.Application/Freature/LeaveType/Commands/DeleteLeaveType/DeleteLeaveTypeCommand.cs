using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Freature.LeaveType.Commands.DeleteLeaveType
{
    public class DeleteLeaveTypeCommand :  IRequest<LeaveTypeDetailsDto>
    {
        public int Id { get; set; } 
    }
}
