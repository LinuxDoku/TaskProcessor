﻿using System;
using TaskProcessor.DI.Contract;

namespace TaskProcessor.DI.Attributes {
    [AttributeUsage(AttributeTargets.Constructor)]
    public class Import : Attribute, IImport {
    }
}
