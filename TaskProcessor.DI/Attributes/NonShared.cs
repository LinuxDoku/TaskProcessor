using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class NonShared : Attribute {
    }
}
