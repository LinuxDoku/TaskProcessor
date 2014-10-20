using System.Reflection;
using System.ServiceProcess;

namespace TaskProcessor.Service {
    public class Service : ServiceBase {
        static void Main() { }

        protected override void OnStart(string[] args) {
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            DI.Container.GetExport<IApplication>();
        }
    }
}
