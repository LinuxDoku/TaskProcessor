﻿using System.Reflection;
using TaskProcessor.Contract;
using TaskProcessor.DI;

namespace TaskProcessor.ConsoleHost {
    public class Program {
        static void Main(string[] args) {
            Container.RegisterAssembly(Assembly.GetAssembly(typeof(Application)));
            Container.RegisterAssembly(Assembly.GetAssembly(typeof(IApplication)));
            var application = Container.GetExport<IApplication>();
            application.Run();
        }
    }
}
