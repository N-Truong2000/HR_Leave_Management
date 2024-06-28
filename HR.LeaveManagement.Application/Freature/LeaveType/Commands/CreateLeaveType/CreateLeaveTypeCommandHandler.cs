using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Logging;
using MediatR;


namespace HR.LeaveManagement.Application.Freature.LeaveType.Commands.CreateLeaveType
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, LeaveTypeDetailsDto>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<CreateLeaveTypeCommandHandler> _logger;
        private readonly IValidator<CreateLeaveTypeCommand> _validator;

        public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<CreateLeaveTypeCommandHandler> logger, IValidator<CreateLeaveTypeCommand> validator)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
            _validator = validator;
        }
        public async Task<LeaveTypeDetailsDto> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate incoming data
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (validationResult.Errors.Any())
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                _logger.LogError($"Validation error in create request for {nameof(LeaveType)}",errors);
                throw new BadRequestException("Invalid Leave type", validationResult);
            }
            // convert to CreateLeaveTypeCommand entity LeaveType
            var CreateLeaveTypeCommandToCeared = _mapper.Map<Domain.LeaveType>(request);
            // add to database
            await _leaveTypeRepository.CreateAsync(CreateLeaveTypeCommandToCeared);

            var LeaveTypeDto = _mapper.Map<LeaveTypeDetailsDto>(CreateLeaveTypeCommandToCeared);
            // returt data
            _logger.LogInformation($"Success {nameof(LeaveType)} {request.Name}");
            return LeaveTypeDto;
        }
    }
}
