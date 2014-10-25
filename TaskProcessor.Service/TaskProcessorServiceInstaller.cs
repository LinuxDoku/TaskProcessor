using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace TaskProcessor.Service {
    [RunInstaller(true)]
    public class TaskProcessorServiceInstaller : Installer {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Configuration.Install.Installer"/> class.
        /// </summary>
        public TaskProcessorServiceInstaller() {
            var serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            var serviceInstaller = new ServiceInstaller();
            serviceInstaller.ServiceName = TaskProcessorService.TaskProcessorServiceName;

            Installers.Add(serviceProcessInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
