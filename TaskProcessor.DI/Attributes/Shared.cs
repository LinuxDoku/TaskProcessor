using System;
using TaskProcessor.DI.Contracts;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Class)]
    public class Shared : Attribute, IShared {
    }
}
