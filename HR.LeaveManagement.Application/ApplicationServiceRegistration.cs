using FluentValidation;
using HR.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using HR.LeaveManagement.Application.Freature.LeaveType.Commands.CreateLeaveType;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;


namespace HR.LeaveManagement.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
            //  services.AddTransient<CreateLeaveTypeCommandValidator>();
            //  services.AddTransient<IValidator<CreateLeaveTypeCommand>, CreateLeaveTypeCommandValidator>();
            services.AddValidatorsFromAssemblyContaining<CreateLeaveTypeCommandValidator>(ServiceLifetime.Transient);
            return services;
        }
    }
}
