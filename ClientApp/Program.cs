// Program.cs
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

public class PCInfo
{
    public string HostName { get; set; }
    public string IPAddress { get; set; }
}

class Program
{
    static async Task Main(string[] args)
    {
        var pcInfo = new PCInfo
        {
            HostName = Environment.MachineName,
            IPAddress = GetLocalIPAddress()
        };

        using var client = new HttpClient();
        var response = await client.PostAsJsonAsync("http://localhost:5254/api/PCInfo", pcInfo);

        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine("PC info sent successfully.");
        }
        else
        {
            Console.WriteLine("Failed to send PC info.");
        }
    }

    static string GetLocalIPAddress()
    {
        foreach (var networkInterface in NetworkInterface.GetAllNetworkInterfaces())
        {
            if (networkInterface.OperationalStatus == OperationalStatus.Up)
            {
                foreach (var address in networkInterface.GetIPProperties().UnicastAddresses)
                {
                    if (address.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.Address.ToString();
                    }
                }
            }
        }
        return "127.0.0.1";
    }
}
