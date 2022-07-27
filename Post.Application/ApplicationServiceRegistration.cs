using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Post.Application.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Post.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new PostProfile());
                cfg.AddProfile(new PostReactProfile());
                cfg.AddProfile(new PostCommentProfile());
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new FriendProfile());

            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());


            //services.AddMediatR(Assembly.GetExecutingAssembly());
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}
