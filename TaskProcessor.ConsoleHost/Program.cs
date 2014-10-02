namespace TaskProcessor.ConsoleHost {
    public class Program {
        static void Main(string[] args) {
            DI.GetExport<IApplication>();
        }
    }
}
