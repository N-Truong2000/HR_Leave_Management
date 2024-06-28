using AutoMapper;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Logging;
using MediatR;



namespace HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetAllLeaveTypes
{
    public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypeQuery, List<LeaveTypeDto>>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

        public GetLeaveTypesQueryHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<GetLeaveTypesQueryHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }
        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypeQuery request, CancellationToken cancellationToken)
        {
            //Query the database
            var leaveTypes = await _leaveTypeRepository.GetAsync();
            //convert data objects to dto objects
            var leaveTypesDto = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

            if (leaveTypesDto == null || leaveTypesDto.Count() == 0)
            {
                _logger.LogError("No content", leaveTypesDto);
                // throw new NotFoundException("No content", leaveTypesDto);
                return leaveTypesDto;
            }
            // return dto
            _logger.LogInformation("Leave types were retrieved successfully", leaveTypesDto);
            return leaveTypesDto;
        }
    }
}
