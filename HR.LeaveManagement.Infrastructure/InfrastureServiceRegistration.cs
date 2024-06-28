using HR.LeaveManagement.Application.Contracts.Email;
using HR.LeaveManagement.Application.Models.Email;
using Microsoft.Extensions.DependencyInjection;
using HR.LeaveManagement.Infrastructure.EmailServices;
using Microsoft.Extensions.Configuration;
using HR.LeaveManagement.Application.Logging;
using HR.LeaveManagement.Infrastructure.Logging;

namespace HR.LeaveManagement.Infrastructure
{
    public static class InfrastureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSetting>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddScoped(typeof(IAppLogger<>),typeof(LoggerAdapter<>));
            return services;
        }
    }
}
