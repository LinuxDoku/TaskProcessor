using System;
using TaskProcessor.DI.Contract;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class Export : Attribute, IExport {
        public Type Type { get; set; }

        public Export() { }

        public Export(string typeName) {
            Type = Type.GetType(typeName);
        }

        public Export(Type type) {
            Type = type;
        }
    }
}
