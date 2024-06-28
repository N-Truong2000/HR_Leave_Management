using AutoFixture.Kernel;
using AutoMapper;
using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Logging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR.LeaveManagement.Application.Freature.LeaveType.Commands.UpdateLeaveType
{
    internal class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, Unit>
    {
        private readonly IMapper _mapper;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IAppLogger<UpdateLeaveTypeCommandHandler> _logger;

        public UpdateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, IAppLogger<UpdateLeaveTypeCommandHandler> logger)
        {
            _mapper = mapper;
            _leaveTypeRepository = leaveTypeRepository;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            // validate incoming data
            var validator = new UpdateLeaveTypeCommandValidator(_leaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            
            if (validationResult.Errors.Any())
            {
                _logger.LogWarning($"Validation error in update request for {nameof(LeaveType)} {request.Id}");
                throw new BadRequestException("Invalid leavetype", validationResult);
            }
            // convert to CreateLeaveTypeCommand entity LeaveType
            // add to database
            var leaveType = await _leaveTypeRepository.GetByIdAsync(request.Id);
            var UpdateLeaveTypeCommandToUpdate = _mapper.Map<Domain.LeaveType>(request);
            UpdateLeaveTypeCommandToUpdate.DateCreated = leaveType.DateCreated;
            await _leaveTypeRepository.UpdateAsync(UpdateLeaveTypeCommandToUpdate);

            return Unit.Value;

        }
    }
}
