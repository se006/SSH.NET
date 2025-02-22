﻿using System.ComponentModel;
using System.Diagnostics;

namespace Renci.SshNet.Abstractions
{
    /// <summary>
    /// Provides access to the <see cref="System.Diagnostics"/> internals of SSH.NET.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public static class DiagnosticAbstraction
    {
#if DEBUG
        /// <summary>
        /// The <see cref="TraceSource"/> instance used by SSH.NET.
        /// </summary>
        /// <remarks>
        /// <para>
        /// Configuration on .NET Core must be done programmatically, e.g.
        /// <code>
        /// DiagnosticAbstraction.Source.Switch = new SourceSwitch("sourceSwitch", "Verbose");
        /// DiagnosticAbstraction.Source.Listeners.Remove("Default");
        /// DiagnosticAbstraction.Source.Listeners.Add(new ConsoleTraceListener());
        /// DiagnosticAbstraction.Source.Listeners.Add(new TextWriterTraceListener("trace.log"));
        /// </code>
        /// </para>
        /// <para>
        /// On .NET Framework, it is possible to configure via App.config, e.g.
        /// <code>
        /// <![CDATA[
        /// <configuration>
        ///     <system.diagnostics>
        ///         <trace autoflush="true"/>
        ///         <sources>
        ///             <source name="SshNet.Logging" switchValue="Verbose">
        ///                 <listeners>
        ///                     <remove name="Default" />
        ///                     <add name="console"
        ///                          type="System.Diagnostics.ConsoleTraceListener" />
        ///                     <add name="logFile"
        ///                          type="System.Diagnostics.TextWriterTraceListener"
        ///                          initializeData="SshNetTrace.log" />
        ///                 </listeners>
        ///             </source>
        ///         </sources>
        ///     </system.diagnostics>
        /// </configuration>
        /// ]]>
        /// </code>
        /// </para>
        /// </remarks>
        public static readonly TraceSource Source = new TraceSource("SshNet.Logging");
#endif

        /// <summary>
        /// Logs a message to <see cref="Source"/> at the <see cref="TraceEventType.Verbose"/>
        /// level.
        /// </summary>
        /// <param name="text">The message to log.</param>
        [Conditional("DEBUG")]
        public static void Log(string text)
        {
            Source.TraceEvent(TraceEventType.Verbose,
                                System.Environment.CurrentManagedThreadId,
                                text);
        }
    }
}
