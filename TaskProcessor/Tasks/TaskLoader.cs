using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TaskProcessor.Contracts;

namespace TaskProcessor.Tasks {
    class TaskLoader {
        public ITask LoadTask(string assembly, string className) {
            return null;
        }

        public ITask LoadTask(string className) {
            return LoadTask(null, className);
        }

        /// <summary>
        /// Load an assembly and get the type from it. When no assembly is given,
        /// use the current executing assembly.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="dllFileName"></param>
        /// <returns></returns>
        private Type GetType(string typeName, string dllFileName = null) {
            Assembly assembly = null;

            if (!string.IsNullOrEmpty(dllFileName) && File.Exists(dllFileName)) {
                assembly = Assembly.LoadFrom(dllFileName);
            }

            if (assembly == null) {
                assembly = Assembly.GetExecutingAssembly();
            }

            return GetType(typeName, assembly);
        }

        /// <summary>
        /// Get type from assembly.
        /// </summary>
        /// <param name="typeName"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private Type GetType(string typeName, Assembly assembly) {
            Type type = null;

            try {
                type = assembly.GetType(typeName);
            } catch (Exception) { }

            return type;
        }
    }
}
