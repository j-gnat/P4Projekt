using log4net;
using log4net.Config;
using log4net.Filter;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]

namespace Generic
{
    public static class Logger
    {
        private static readonly ILog s_log = LogManager.GetLogger(typeof(Logger));
        public static void InfoLog(string message)
        {
            s_log.Info(message);
        }

        public static void ErrorLog(string message)
        {
            s_log.Error(message);
        }

        public static void DebugLog(string message)
        {
            s_log.Debug(message);
        }

        public static void WarnLog(string message)
        {
            s_log.Warn(message);
        }
    }
}
