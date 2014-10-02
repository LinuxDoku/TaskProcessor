using System;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using TaskProcessor.Contracts;

namespace TaskProcessor {
    public class DI {
        private static DI _instance;
        private CompositionHost Container { get; set; }

        private DI() {
            var container = new ContainerConfiguration();
            container.WithAssembly(Assembly.GetExecutingAssembly());
            
            // load tasks
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var tasksPath = path + "\\tasks";
            LoadTaskAssemblies(container, tasksPath);

            Container = container.CreateContainer();
        }

        private static DI GetInstance() {
            if (DI._instance == null) {
                DI._instance = new DI();
            }

            return DI._instance;
        }

        public static T GetExport<T>() {
            return DI.GetInstance().Container.GetExport<T>();
        }

        /// <summary>
        /// Load task assemblies from a specified path.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="path"></param>
        private static void LoadTaskAssemblies(ContainerConfiguration container, string path) {
            try {
                if (Directory.Exists(path)) {
                    var dlls = Directory.GetFiles(path).Where(x => x.EndsWith(".dll"));

                    foreach (var dll in dlls) {
                        var assembly = Assembly.LoadFrom(dll);

                        if (assembly.ExportedTypes.Contains(typeof (ITask))) {
                            container.WithAssembly(assembly);
                        }
                    }
                }
            } catch (Exception) { }
        }
    }
}
