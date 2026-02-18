using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace yoma;

public class NetworkChecks
{
    public static async Task<bool> IsReadyWithNetwork()
    {
        // Example: ping Google once
        return await PingGoogle(1);
    }

    public bool IsConnectedToNetwork()
    {
        try
        {
            foreach (var ni in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces())
            {
                if (ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
                    return true;
            }
        }
        catch { }

        return false;
    }

    public static async Task<bool> PingGoogle(int pingTimes = 1)
    {
        try
        {
            using var ping = new Ping();
            for (int i = 0; i < pingTimes; i++)
            {
                var reply = await ping.SendPingAsync("8.8.8.8");
                if (reply.Status != IPStatus.Success)
                    return false;
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
    
    public static async Task<bool> TestConnection()
    {
        return await PingGoogle(3); // test with 3 pings
    }
}