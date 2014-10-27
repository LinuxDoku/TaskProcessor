using System.Reflection;
using TaskProcessor.Communication.Contract;
using TaskProcessor.DI;

namespace TaskProcessor.ConsoleHost {
    public class Program {
        static void Main(string[] args) {
            Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            Application.InitializeContainer();
            Container.GetExport<IApplication>();
        }
    }
}
