namespace Utilities
{
    using System;
    using System.Reflection;

    public static class YoLog
    {
        public static void Info(string msg)
        {
            Log("INFO", msg, Assembly.GetCallingAssembly().GetName().Name);
        }
        
        public static void Debug(string msg)
        {
            Log("DEBUG", msg, Assembly.GetCallingAssembly().GetName().Name);
        }

        private static void Log(string level, string msg, string assemblyName)
        {
            Console.WriteLine($"[{assemblyName}:{level}] {msg}");
        }
    }
}