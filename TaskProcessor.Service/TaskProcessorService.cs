using System;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace TaskProcessor.Service {
    public class TaskProcessorService : ServiceBase {
        public static string ServiceName = "TaskProcessor";

        static void Main(string[] args) {
            if (args.Any()) {
                if (args.First() == "install" && ServiceController.GetServices().All(x => x.ServiceName != ServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] {"/i", Assembly.GetExecutingAssembly().Location});
                }

                if (args.First() == "uninstall" && ServiceController.GetServices().Any(x => x.ServiceName == ServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
                }
            }
        }

        protected override void OnStart(string[] args) {
            var thread = new Thread(RunApplication);
            thread.IsBackground = false;
            thread.Start();
        }

        private void RunApplication() {
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            DI.Container.GetExport<IApplication>();
        }
    }
}
