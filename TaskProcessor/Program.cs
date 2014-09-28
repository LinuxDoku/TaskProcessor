using System;

namespace TaskProcessor {
    class Program {
        public static void Main(string[] args) {
            DI.GetExport<IApplication>();
        }
    }
}