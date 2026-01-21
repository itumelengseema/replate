using Microsoft.Extensions.DependencyInjection;

using Replate.Application.Common.Vendors.CreateVendorProfile;

namespace Replate.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //Register AutoMapper
      //  services.AddAutoMapper(typeof(DependencyInjection).Assembly);
   
        services.AddScoped<CreateVendorProfileHandler>();
        return services;
    }
}