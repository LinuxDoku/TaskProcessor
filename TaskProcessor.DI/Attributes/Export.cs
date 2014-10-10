using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class Export : Attribute {
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
