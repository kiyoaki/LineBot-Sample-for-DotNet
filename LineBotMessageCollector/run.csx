using System.Net;
using System.Threading.Tasks;

public static void Run(HttpRequestMessage req, TraceWriter log, out string lineBotQueueItem)
{
    lineBotQueueItem = req.Content.ReadAsStringAsync().Result;
}