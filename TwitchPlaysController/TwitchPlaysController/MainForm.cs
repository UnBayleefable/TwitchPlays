using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchPlaysController;

public partial class MainForm : Form
{
    private bool isRunning = false;
    private bool isPaused = false;
    private CancellationTokenSource cts;
    private Task listeningTask;
    private ConnectionProvider twitch;

    public MainForm()
    {
        InitializeComponent();
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        if (isRunning)
        {
            MessageBox.Show("Already running!");
            return;
        }
        AppendConsole("Starting Listener");
        isRunning = true;
        isPaused = false;

        btnPause.Enabled = isRunning;
        btnStop.Enabled = isRunning;
        btnStart.Enabled = !isRunning;

        twitch = new ConnectionProvider();
        await twitch.ConnectAsync("UnBayleefable"); // TODO: your channel here

        cts = new CancellationTokenSource();
        listeningTask = Task.Run(() => ListenLoop(cts.Token));
        AppendConsole("Listener started");
    }

    private void btnStop_Click(object sender, EventArgs e)
    {
        if (!isRunning)
            return;

        AppendConsole("Stopping Listener");
        cts.Cancel();

        isRunning = false;
        isPaused = false;

        btnPause.Enabled = isRunning;
        btnStop.Enabled = isRunning;
        btnStart.Enabled = !isRunning;
    }

    private void btnPause_Click(object sender, EventArgs e)
    {
        if (!isRunning)
            return;

        isPaused = !isPaused; // toggle pause
        isRunning = !isRunning;

        btnPause.Enabled = isRunning;
        btnStop.Enabled = isRunning;
        btnStart.Enabled = !isRunning;

        btnPause.Text = isPaused ? "Resume" : "Pause";
        AppendConsole($"{btnPause.Text} Listener");
    }

    private async void ListenLoop(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            if (isPaused)
            {
                await Task.Delay(100);
                continue;
            }

            var newMessages = await twitch.ReceiveMessagesAsync();
            if (newMessages.Count > 0)
            {
                foreach (var msg in newMessages)
                {
                    AppendConsole(msg);

                    // Lowercase message
                    string text = msg.ToLower();

                    if (text.Contains(": left"))
                    {
                        Task.Run(() => InputSimulator.HoldAndReleaseKey(KeyCodes.A, 2000)); // 2 sec
                    }
                    else if (text.Contains(": right"))
                    {
                        Task.Run(() => InputSimulator.HoldAndReleaseKey(KeyCodes.D, 2000));
                    }
                    else if (text.Contains(": drive"))
                    {
                        Task.Run(() => InputSimulator.HoldKey(KeyCodes.W));
                    }
                    else if (text.Contains(": stop"))
                    {
                        Task.Run(() => InputSimulator.ReleaseKey(KeyCodes.W));
                        Task.Run(() => InputSimulator.ReleaseKey(KeyCodes.S));
                    }
                    else if (text.Contains(": brake"))
                    {
                        Task.Run(() => InputSimulator.HoldAndReleaseKey(KeyCodes.SPACE, 700));
                    }
                    else if (text.Contains(": shoot"))
                    {
                        Task.Run(() =>
                        {
                            InputSimulator.MouseLeftDown();
                            Thread.Sleep(1000);
                            InputSimulator.MouseLeftUp();
                        });
                    }
                    else if (text.Contains(": aim up"))
                    {
                        Task.Run(() => InputSimulator.MoveMouse(0, -30));
                    }
                    else if (text.Contains(": aim down"))
                    {
                        Task.Run(() => InputSimulator.MoveMouse(0, 30));
                    }
                    else if (text.Contains(": aim right"))
                    {
                        Task.Run(() => InputSimulator.MoveMouse(30, 0));
                    }
                    else if (text.Contains(": aim left"))
                    {                        
                        Task.Run(() => InputSimulator.MoveMouse(-30, 0));
                    }
                }
            }
        }
    }

    private void AppendConsole(string text)
    {
        if (tboConsole.InvokeRequired)
        {
            tboConsole.Invoke(new Action<string>(AppendConsole), text);
        }
        else
        {
            tboConsole.AppendText(text + Environment.NewLine);
        }
    }
}
