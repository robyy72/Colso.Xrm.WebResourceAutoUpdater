using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Colso.Xrm.WebResourceAutoUpdater.AppCode
{
    public class StatusMessageEventArgs : EventArgs
    {
        public string Message { get; private set; }

        public StatusMessageEventArgs(string message)
        {
            Message = message;
        }

        public StatusMessageEventArgs(string message, params object[] args)
        {
            Message = string.Format(message, args);
        }
    }
}
