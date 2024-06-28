using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetAllLeaveTypes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using HR.LeaveManagement.Application.Freature.LeaveType.Queries.GetLeaveTypeDetails;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.CreateLeaveType;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.UpdateLeaveType;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.DeleteLeaveType;
using HR.LeaveManagement.Application.Models;
using Azure.Core;
using HR.LeaveManagement.Domain;
using Microsoft.AspNetCore.Authorization;
using SendGrid.Helpers.Errors.Model;
using HR.LeaveManagement.Application.Exceptions;
namespace HR.LeaveManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class LeaveTypesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
        {
          
            var leaveTypes = await _mediator.Send(new GetLeaveTypeQuery());
            if (leaveTypes == null || leaveTypes.Count == 0)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "No data available");
            }
            return Ok(leaveTypes);
        }
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Get(int id)
        {
            var leaveTypes = await _mediator.Send(new GetLeaveTypeDetailsQuery(id));
            if (leaveTypes == null)
            {
                return Problem(statusCode: StatusCodes.Status404NotFound, detail: "No data available", type: null);
            }
            return Ok(leaveTypes);
        }
        [HttpPost()]
        [ProducesResponseType(201)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Post([FromBody] CreateLeaveTypeCommand leaveType)
        {
            var response = await _mediator.Send(leaveType);
            return CreatedAtAction(nameof(Get), response);
        }
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Put([FromBody] UpdateLeaveTypeCommand leaveType, [FromRoute] int id)
        {
            leaveType.Id = id;
            await _mediator.Send(leaveType);
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<LeaveTypeDetailsDto>> Delete(int id)
        {
            var command = new DeleteLeaveTypeCommand { Id = id };
            var leaveTypes = await _mediator.Send(command);
            return NoContent();
        }
    }
}
