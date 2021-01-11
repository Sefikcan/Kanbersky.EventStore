using FluentValidation;
using Kanbersky.EventStore.Services.Abstract;
using Kanbersky.EventStore.Services.Commands;
using Kanbersky.EventStore.Services.Concrete;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kanbersky.EventStore.Services.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServiceLayer(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient<IValidator<AssignTaskRequest>, AssignTaskRequestValidator>();
            services.AddTransient<IValidator<ChangeTaskStatusRequest>, ChangeTaskStatusRequestValidator>();
            services.AddTransient<IValidator<CreateTaskRequest>, CreateTaskRequestValidator>();

            services.AddScoped<ITaskContentAggregate, TaskContentAggregate>();

            return services;
        }
    }
}
