using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logger
{
    public interface ILog
    {
        void LoggerMessage(string message, int level);
        string GetLogValue(int level);
        string GetLocalIPAddress();
        void SetUsername(string username);
    }
}
