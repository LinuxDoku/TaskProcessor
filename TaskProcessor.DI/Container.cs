using System;
using System.Linq;
using System.Reflection;
using Autofac;
using TaskProcessor.DI.Attributes;

namespace TaskProcessor.DI {
    public class Container {
        private static Container _instance = new Container();
        private readonly IContainer _container;

        private delegate Type TypeOfExport(Type type);
        private TypeOfExport _defaultTypeOfExport;

        private Container() {
            _defaultTypeOfExport = x => x.GetCustomAttributes(typeof (Export), true).OfType<Export>().First().Type ?? x;

            var containerBuilder = new ContainerBuilder();

            // load current assembly
            RegisterAssembly(containerBuilder, Assembly.GetExecutingAssembly());

            _container = containerBuilder.Build();
        }

        public static T GetExport<T>() {
            return _instance._container.Resolve<T>();
        }

        public static object GetExport(Type type) {
            object result;
            if (_instance._container.TryResolve(type, out result)) {
                return result;
            }
            return null;
        }

        public static void RegisterAssembly(Assembly assembly) {
            if (assembly == null) return;

            var containerBuilder = new ContainerBuilder();
            _instance.RegisterAssembly(containerBuilder, assembly);
            containerBuilder.Update(_instance._container);
        }

        private void RegisterAssembly(ContainerBuilder builder, Assembly assembly) {
            var types = assembly.GetTypes()
                                .Where(x => x.GetCustomAttributes(typeof (Export), true).Any())
                                .ToArray();

            builder.RegisterTypes(types)
                .Where(x => x.GetCustomAttributes(typeof (Shared), true).Any())
                .As(x => _defaultTypeOfExport(x))
                .SingleInstance();

            builder.RegisterTypes(types)
                .Where(
                    x => !x.GetCustomAttributes(typeof (Shared), true).Any() ||
                          x.GetCustomAttributes(typeof (NonShared), true).Any())
                .As(x => _defaultTypeOfExport(x))
                .InstancePerDependency();
        }
    }
}
