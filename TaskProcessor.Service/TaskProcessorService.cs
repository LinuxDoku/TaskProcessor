using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace TaskProcessor.Service {
    public class TaskProcessorService : ServiceBase {
        public static string ServiceName = "TaskProcessor";

        static void Main() {
            if (ServiceController.GetServices().All(x => x.ServiceName != ServiceName)) {
                ManagedInstallerClass.InstallHelper(new[] {Assembly.GetExecutingAssembly().Location});
            }
        }

        protected override void OnStart(string[] args) {
            new Thread(RunApplication).Start();
        }

        private void RunApplication() {
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            DI.Container.GetExport<IApplication>();
        }
    }
}
