using System.Composition.Hosting;
using System.Reflection;

namespace TaskProcessor {
    public class DI {
        private static DI _instance;
        public CompositionHost Container { get; private set; }

        public DI() {
            var container = new ContainerConfiguration();
            container.WithAssembly(Assembly.GetExecutingAssembly());

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
    }
}
