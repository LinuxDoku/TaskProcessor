using System;
using TaskProcessor.DI.Contract;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class ExportAttribute : Attribute, IExport {
        public Type Type { get; set; }

        public ExportAttribute() {}

        public ExportAttribute(string typeName) {
            Type = Type.GetType(typeName);
        }

        public ExportAttribute(Type type) {
            Type = type;
        }
    }
}
