using System;
using TaskProcessor.DI.Contracts;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Constructor)]
    public class Import : Attribute, IImport {
    }
}
