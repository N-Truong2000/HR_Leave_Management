using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Freature.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommand : IRequest<LeaveTypeDetailsDto>
    {
        public string Name {  get; set; }=string.Empty;
        public int DefaultDays {  get; set; }
    }
}
