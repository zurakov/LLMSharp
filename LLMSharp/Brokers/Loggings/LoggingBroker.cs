// ---------------------------------------------------------------
// Copyright (c) Zafar Urakov. All rights reserved.
// Licensed under the MIT License.
// ---------------------------------------------------------------

using System;

namespace LLMSharp.Brokers.Loggings
{
    public class LoggingBroker : ILoggingBroker
    {
        public void LogInformation(string message) =>
            Console.WriteLine($"[INFO] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");

        public void LogWarning(string message) =>
            Console.WriteLine($"[WARN] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");

        public void LogError(string message) =>
            Console.WriteLine($"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");

        public void LogError(Exception exception)
        {
            Console.WriteLine($"[ERROR] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {exception.Message}");
            Console.WriteLine($"Stack Trace: {exception.StackTrace}");
        }

        public void LogDebug(string message) =>
            Console.WriteLine($"[DEBUG] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");

        public void LogTrace(string message) =>
            Console.WriteLine($"[TRACE] {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss} - {message}");
    }
}
