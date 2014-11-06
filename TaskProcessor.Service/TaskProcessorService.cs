using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using TaskProcessor.Contract;

namespace TaskProcessor.Service {
    public partial class TaskProcessorService : ServiceBase {
        /// <summary>
        /// Service entry point with integrated cli installer.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args) {
            if (args.Any() && args.First() != "") {
                if (args.First() == "run") {
                    var service = new TaskProcessorService();
                    service.RunApplication();
                }

                if (args.First() == "install" && ServiceController.GetServices().All(x => x.ServiceName != TaskProcessorServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] { "/i", Assembly.GetExecutingAssembly().Location });
                }

                if (args.First() == "uninstall" &&  ServiceController.GetServices().Any(x => x.ServiceName == TaskProcessorServiceName)) {
                    ManagedInstallerClass.InstallHelper(new[] { "/u", Assembly.GetExecutingAssembly().Location });
                }
            } else {
                Run(new TaskProcessorService());
            }
        }

        /// <summary>
        /// Creates a new instance of the <see cref="T:System.ServiceProcess.ServiceBase"/> class.
        /// </summary>
        public TaskProcessorService() {
            ServiceName = TaskProcessorServiceName;

            if (!EventLog.SourceExists(ServiceName)) {
                EventLog.CreateEventSource(ServiceName, "Application");
            }

            EventLog.Source = ServiceName;
        }

        public static string TaskProcessorServiceName = "TaskProcessor";

        protected override void OnStart(string[] args) {
#if DEBUG
            Debugger.Launch();
#endif
            var thread = new Thread(RunApplication);
            thread.IsBackground = false;
            thread.Start();
        }

        private void RunApplication() {
            EventLog.WriteEntry("RunApplication");
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(Application)));
            DI.Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            var application = DI.Container.GetExport<IApplication>();
            application.Run();
            EventLog.WriteEntry("RunApplication End");
        }
    }
}
