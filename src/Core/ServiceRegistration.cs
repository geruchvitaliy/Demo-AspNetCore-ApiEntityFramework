using Autofac;
using Common.Attributes;
using System;
using System.Reflection;

namespace Core
{
    static class ServiceRegistration
    {
        public static ContainerBuilder RegisterServices<T>(this ContainerBuilder builder)
        {
            foreach (var type in typeof(T).Assembly.GetTypes())
                builder.Register(type);

            return builder;
        }

        static void Register(this ContainerBuilder builder, Type type)
        {
            if (!type.IsClass)
                return;

            if (type.IsAbstract)
                return;

            if (!type.IsDefined(typeof(ServiceAttribute)))
                return;

            var service = type.GetCustomAttribute<ServiceAttribute>();

            if (type.IsGenericType)
            {
                if (service.AsSelf && service.AsSingleton)
                    builder.RegisterGeneric(type).AsSelf().SingleInstance();

                if (service.AsSelf && !service.AsSingleton)
                    builder.RegisterGeneric(type).AsSelf().InstancePerLifetimeScope();

                if (!service.AsSelf && service.AsSingleton)
                    builder.RegisterGeneric(type).AsImplementedInterfaces().SingleInstance();

                if (!service.AsSelf && !service.AsSingleton)
                    builder.RegisterGeneric(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            }
            else
            {
                if (service.AsSelf && service.AsSingleton)
                    builder.RegisterType(type).AsSelf().SingleInstance();

                if (service.AsSelf && !service.AsSingleton)
                    builder.RegisterType(type).AsSelf().InstancePerLifetimeScope();

                if (!service.AsSelf && service.AsSingleton)
                    builder.RegisterType(type).AsImplementedInterfaces().SingleInstance();

                if (!service.AsSelf && !service.AsSingleton)
                    builder.RegisterType(type).AsImplementedInterfaces().InstancePerLifetimeScope();
            }
        }
    }
}