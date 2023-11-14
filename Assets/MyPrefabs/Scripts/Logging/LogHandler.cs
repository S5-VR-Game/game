using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace MyPrefabs.Scripts.Logging
{
    public class LogHandler : ILogHandler
    {
        public void LogFormat(LogType logType, Object context, string format, params object[] args)
        {
            // log using default unity logger
            Debug.unityLogger.logHandler.LogFormat(logType, context, format, args);
        }

        public void LogException(Exception exception, Object context)
        {
            // log using default unity logger
            Debug.unityLogger.LogException(exception, context);
        }
    }
}
