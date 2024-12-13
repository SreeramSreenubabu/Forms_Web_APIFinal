using System;
using System.Configuration;
using System.IO;
using System.Net.Sockets;
using System.Net;
using Logger;
using WindowsFormApp;

namespace WindowsFormApp.Logger
{
    public class Log : ILog
    {
        private static readonly string logDirectoryPath = ConfigurationManager.AppSettings["LogPath"];
        private static readonly int logLevel = int.Parse(ConfigurationManager.AppSettings["LogLevel"]);
        private string currentUsername;

        public void SetUsername(string username)
        {
            currentUsername = username;
        }

        public void LoggerMessage(string message, int level)
        {
            try
            {
                if (!Directory.Exists(logDirectoryPath))
                {
                    Directory.CreateDirectory(logDirectoryPath);
                }

                if (level <= logLevel)
                {
                    string logMessage = $"[{DateTime.Now}] : [{currentUsername ?? $"{Environment.UserName}"}] : {GetLogValue(level)} : {message}";
                    string logFileName = $"Log ({DateTime.Now:dd-MM-yyyy}).txt";
                    string logFilePath = Path.Combine(logDirectoryPath, logFileName);

                    using (StreamWriter writer = new StreamWriter(logFilePath, true))
                    {
                        writer.WriteLine(logMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while logging: {ex.Message}");
            }
        }
    
        public string GetLogValue(int level)
        {
            switch (level)
            {
                case 1:
                    return "[INFO]";
                case 2:
                    return "[WARN]";
                case 3:
                    return "[ERRO]";
                default:
                    return "[UNKN]";
            }
        }

        public string GetLocalIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress iPAddress in hostEntry.AddressList)
            {
                if (iPAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    return iPAddress.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}