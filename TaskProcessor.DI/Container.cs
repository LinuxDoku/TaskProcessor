﻿using System;
using System.Linq;
using System.Reflection;
using Autofac;
using TaskProcessor.DI.Attributes;
using ContainerInterface=TaskProcessor.DI.Contract.IContainer;
using System.Collections.Generic;

namespace TaskProcessor.DI {
    /// <summary>
    /// Dependency injection container.
    /// 
    /// Allows injection an exporting all services which are declared with an Export attribute.
    /// </summary>
    public class Container : Contract.IContainer {
        private static Container _instance = new Container();
        private readonly IContainer _container;

        private delegate Type[] TypeOfExport(Type type);
        private TypeOfExport _defaultTypeOfExport;

        private Container() {
            _defaultTypeOfExport = x => {
                var types = new List<Type>();
                var exports = x.GetCustomAttributes(typeof (ExportAttribute), true)
                               .OfType<ExportAttribute>();

                if (exports.Any()) {
                    foreach (var export in exports) {
                        types.Add(export.Type);
                    }
                } else {
                    types.Add(x);
                }

                return types.ToArray();
            };

            var containerBuilder = new ContainerBuilder();

            // load current assembly
            RegisterAssembly(containerBuilder, Assembly.GetExecutingAssembly());

            _container = containerBuilder.Build();
        }

        /// <summary>
        /// Get single export for type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetExport<T>() {
            return _instance._container.Resolve<T>();
        }

        /// <summary>
        /// Get all exports for type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IEnumerable<T> GetExports<T>() {
            return GetExport<IEnumerable<T>>();
        }

        /// <summary>
        /// Get a single export for type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetExport(Type type) {
            object result;
            if (_instance._container.TryResolve(type, out result)) {
                return result;
            }
            return null;
        }

        /// <summary>
        /// Get all exports for type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IEnumerable<object> GetExports(Type type) {
            object results;
            if(_instance._container.TryResolve(typeof(IEnumerable<>).MakeGenericType(type), out results)) {
                return (IEnumerable<object>)results;
            }
            return null;
        }

        public static void Register<T>(Type type) {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterType(type).As<T>();
            containerBuilder.Update(_instance._container);
        }

        /// <summary>
        /// Register all types of the assembly which have an Export attribute.
        /// </summary>
        /// <param name="assembly"></param>
        public static void RegisterAssembly(Assembly assembly) {
            if (assembly == null) return;

            var containerBuilder = new ContainerBuilder();
            _instance.RegisterAssembly(containerBuilder, assembly);
            containerBuilder.Update(_instance._container);
        }

        private void RegisterAssembly(ContainerBuilder builder, Assembly assembly) {
            var types = assembly.GetTypes()
                                .Where(x => x.GetCustomAttributes(typeof (ExportAttribute), true).Any())
                                .ToArray();

            builder.RegisterTypes(types)
                .Where(x => x.GetCustomAttributes(typeof (SharedAttribute), true).Any())
                .As(x => _defaultTypeOfExport(x))
                .SingleInstance();

            builder.RegisterTypes(types)
                .Where(
                    x => !x.GetCustomAttributes(typeof (SharedAttribute), true).Any() ||
                          x.GetCustomAttributes(typeof (NonSharedAttribute), true).Any())
                .As(x => _defaultTypeOfExport(x))
                .InstancePerDependency();
        }
    }
}
