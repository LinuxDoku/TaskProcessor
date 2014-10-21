using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace TaskProcessor.Service {
    public class Service : ServiceBase {
        static void Main() { }

        protected override void OnStart(string[] args) {
            new Thread(RunApplication).Start();
        }

        private void RunApplication() {
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            DI.Container.GetExport<IApplication>();
        }
    }
}
