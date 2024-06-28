using AutoMapper;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetAllLeaveTypes;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.MappingProfiles
{
    public class LeaveTypeProfile : Profile
    {
        public LeaveTypeProfile()
        {
            CreateMap<LeaveType, LeaveTypeDto>().ReverseMap();
            CreateMap<LeaveType, LeaveTypeDetailsDto>().ReverseMap();
            CreateMap<CreateLeaveTypeCommand, LeaveTypeDetailsDto>().ReverseMap();
            CreateMap<CreateLeaveTypeCommand, LeaveType>();
            CreateMap<UpdateLeaveTypeCommand, LeaveType>()
                .ForMember(x => x.DateCreated, y => y.Ignore());
        }
    }
}
