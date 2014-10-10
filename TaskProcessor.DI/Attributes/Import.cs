using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Constructor)]
    public class Import : Attribute {
    }
}
