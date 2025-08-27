using System;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;


namespace TwitchPlaysController;
public class ConnectionProvider
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;
    private string channel;
    private bool connected = false;

    public async Task ConnectAsync(string channelName, string oauthToken = null)
    {
        this.channel = channelName;

        client = new TcpClient();
        await client.ConnectAsync("irc.chat.twitch.tv", 6667);

        reader = new StreamReader(client.GetStream());
        writer = new StreamWriter(client.GetStream()) { AutoFlush = true };

        string username;
        if (string.IsNullOrEmpty(oauthToken))
        {
            // Anonymous login
            username = "justinfan" + new Random().Next(10000, 99999);
            await writer.WriteLineAsync("PASS SCHMOOPIIE");  // Twitch accepts this dummy password
        }
        else
        {
            // Authenticated login
            username = "mybot"; // TODO: replace with your bot's username
            await writer.WriteLineAsync($"PASS oauth:{oauthToken}");
        }

        await writer.WriteLineAsync($"NICK {username}");
        await writer.WriteLineAsync($"JOIN #{channel}");

        connected = true;
        Console.WriteLine($"Connected to Twitch IRC, joined #{channel}");
    }


    public async Task<List<string>> ReceiveMessagesAsync()
    {
        var messages = new List<string>();
        if (!connected || reader == null)
            return messages;

        // read one line, wait until Twitch sends something
        string line = await reader.ReadLineAsync();
        if (line == null)
            return messages;

        // Handle PING
        if (line.StartsWith("PING"))
        {
            await writer.WriteLineAsync("PONG :tmi.twitch.tv");
            return messages;
        }

        // Example line:
        // :username!username@username.tmi.twitch.tv PRIVMSG #channel :hello world
        if (line.Contains("PRIVMSG"))
        {
            int excl = line.IndexOf('!');
            int col = line.IndexOf(" :", StringComparison.Ordinal);
            if (excl > 0 && col > 0)
            {
                string user = line.Substring(1, excl - 1);
                string msg = line.Substring(col + 2);
                messages.Add($"{user}: {msg}");
            }
        }

        return messages;
    }

    public async Task<string?> ReadRawLineAsync(CancellationToken token)
    {
        if (!connected || reader == null)
            return null;

        try
        {
            using (token.Register(() => client.Close())) // force break if cancelled
            {
                return await reader.ReadLineAsync();
            }
        }
        catch (IOException)
        {
            return null; // stream closed
        }
    }

    public void Disconnect()
    {
        connected = false;
        reader?.Close();
        writer?.Close();
        client?.Close();
    }

    public async Task SendPongAsync()
    {
        if (writer != null)
            await writer.WriteLineAsync("PONG :tmi.twitch.tv");
    }
}
