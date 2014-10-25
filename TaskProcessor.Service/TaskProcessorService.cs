using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;

namespace TaskProcessor.Service {
    public partial class TaskProcessorService : ServiceBase {
        /// <summary>
        /// Creates a new instance of the <see cref="T:System.ServiceProcess.ServiceBase"/> class.
        /// </summary>
        public TaskProcessorService() {
            ServiceName = TaskProcessorServiceName;
        }

        public static string TaskProcessorServiceName = "TaskProcessor";

        static void Main(string[] args) {
            if (args.Any() && args.First() != "") {
                if (args.First() == "install") {
                    ManagedInstallerClass.InstallHelper(new[] {"/i", Assembly.GetExecutingAssembly().Location});
                }

                if (args.First() == "uninstall" &&
                    ServiceController.GetServices().Any(x => x.ServiceName == TaskProcessorServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] {"/u", Assembly.GetExecutingAssembly().Location});
                }
            } else {
                Run(new TaskProcessorService());
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
