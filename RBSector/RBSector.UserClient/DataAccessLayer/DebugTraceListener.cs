using SQLite.Net;
using System.Diagnostics;

namespace RBSector.UserClient.DataAccessLayer
{
    public class DebugTraceListener : ITraceListener
    {
        public void Receive(string message)
        {
            Debug.WriteLine(message);
        }
    }
}
