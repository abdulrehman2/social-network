using AutoMapper;
//using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Identity.Application.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new FriendProfile());
                cfg.AddProfile(new FriendRequestProfile());
                cfg.AddProfile(new UserProfile());

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
