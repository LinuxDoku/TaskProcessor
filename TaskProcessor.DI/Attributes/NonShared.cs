using System;
using TaskProcessor.DI.Contract;

namespace TaskProcessor.DI.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NonShared : Attribute, INonShared {
    }
}
