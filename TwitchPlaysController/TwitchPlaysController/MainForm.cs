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
                btnPause.Enabled = isRunning;
                btnStop.Enabled = isRunning;
                btnStart.Enabled = !isRunning;
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
        if (isRunning)
        {
            try
            {
                pythonProcess.Kill();
                pipeServer.Dispose();
                isRunning = false;

                btnPause.Enabled = isRunning;
                btnStop.Enabled = isRunning;
                btnStart.Enabled = !isRunning;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping script: {ex.Message}");
            }
        }
    }

    private void btnPause_Click(object sender, EventArgs e)
    {
        if (isRunning)
        {
            try
            {
                pythonProcess.Kill();
                pipeServer.Dispose();
                isRunning = false;

                btnPause.Enabled = isRunning;
                btnStop.Enabled = isRunning;
                btnStart.Enabled = !isRunning;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error stopping script: {ex.Message}");
            }
        }
    }
}
