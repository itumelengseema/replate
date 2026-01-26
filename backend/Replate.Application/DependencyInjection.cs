using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Replate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services){
        // Add Application Services 
        
        //MediatR
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            
       // AutoMapper
       services.AddAutoMapper(Assembly.GetExecutingAssembly());
       
       // Validation
       services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}