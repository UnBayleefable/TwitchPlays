using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchPlaysController;

public partial class MainForm : Form
{
    private Process pythonProcess;
    private NamedPipeServerStream pipeServer;
    private bool isRunning = false;

    public MainForm()
    {
        InitializeComponent();
        /*// Initialize controls
        this.Size = new Size(400, 300);
        this.Text = "Twitch Plays Controller";

        var startButton = new Button
        {
            Text = "Start Script",
            Location = new Point(20, 20),
            Size = new Size(100, 30),
            Visible = !isRunning
        };
        startButton.Click += StartScript_Click;

        var stopButton = new Button
        {
            Text = "Stop Script",
            Location = new Point(130, 20),
            Size = new Size(100, 30),
            Visible = isRunning

        };
        stopButton.Click += StopScript_Click;

        var statusLabel = new Label
        {
            AutoSize = true,
            Location = new Point(20, 70)
        };

        Controls.AddRange(new Control[] { startButton, stopButton, statusLabel });*/
    }

    private async void btnStart_Click(object sender, EventArgs e)
    {
        if (!isRunning)
        {
            try
            {
                pipeServer = new NamedPipeServerStream(
                    "twitch_plays_pipe",
                    PipeDirection.InOut,
                    1,
                    PipeTransmissionMode.Byte);

                await Task.Run(() => pipeServer.WaitForConnection());
                StartPythonProcess();
                isRunning = true;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting script: {ex.Message}");
            }
        }
    }

    private void StartPythonProcess()
    {
        pythonProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "python.exe",
                Arguments = $"script.py twitch_plays_pipe",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            }
        };
        pythonProcess.Start();
    }

    private void btnStop_Click(object sender, EventArgs e)
    {

    }

    private void btnPause_Click(object sender, EventArgs e)
    {

    }
}
