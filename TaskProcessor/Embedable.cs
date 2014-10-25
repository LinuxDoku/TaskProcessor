namespace TaskProcessor
{
    /// <summary>
    ///     Workaround for compiler optimization, which removes runtime loaded assemblies.
    /// </summary>
    internal sealed class Embedable
    {
// ReSharper disable UnusedField.Compiler
        private Microsoft.Owin.Host.HttpListener.OwinHttpListener owinHttpListener = null;
        private Microsoft.Owin.Security.ICertificateValidator owinSecurity = null;
// ReSharper restore UnusedField.Compiler
    }
}
