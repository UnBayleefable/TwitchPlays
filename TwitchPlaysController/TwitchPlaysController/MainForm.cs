using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using TwitchPlaysController.Models;

namespace TwitchPlaysController;

public partial class MainForm : Form
{
    private Dictionary<string, KeyBinding> commandBindings = new();
    private const string BINDINGS_FILE = "keybindings.json";

    private bool isRunning = false;
    private bool isPaused = false;

    private CancellationTokenSource cts;
    private Task listeningTask;
    private ConnectionProvider twitch;

    private BindingConfig currentConfig = new();
    private KeyMapPreset activePreset;

    public MainForm()
    {
        InitializeComponent();
        LoadPresets();
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
                    await HandleCommand(msg); // Note: Now async
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

    private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // open settings form
        using (var settingsForm = new frmSettings())
        {
            settingsForm.ShowDialog();
        }
    }

    private void exitToolStripMenuItem_Click(object sender, EventArgs e)
    {
        // dispose and exit
        if (isRunning)
            btnStop_Click(null, null);

        Application.Exit();
    }

    private void LoadPresets()
    {        

        try
        {
            if (!File.Exists(BINDINGS_FILE))
                InitializeDefaultBindings();
            
            string json = File.ReadAllText(BINDINGS_FILE);
            currentConfig = JsonSerializer.Deserialize<BindingConfig>(json);

            // Populate presets dropdown
            cboKeyPreset.Items.Clear();
            cboKeyPreset.Items.AddRange(currentConfig.Presets.Select(p => p.Name).ToArray());

            // Select first preset if any exist
            if (currentConfig.Presets.Count > 0)
            {
                cboKeyPreset.SelectedIndex = 0;
            }             
            
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading presets: {ex.Message}");
            currentConfig.Presets.Add(new KeyMapPreset { Name = "Default" });
        }
    }

    private void SavePresets()
    {
        try
        {
            string json = JsonSerializer.Serialize(currentConfig, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(BINDINGS_FILE, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saving presets: {ex.Message}");
        }
    }

    private void cboKeyPreset_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (sender is ComboBox comboBox && comboBox.SelectedItem is string selectedName)
        {
            activePreset = currentConfig.Presets.FirstOrDefault(p => p.Name == selectedName);
            UpdateCommandBindings();
        }
    }
    private void UpdateCommandBindings()
    {
        commandBindings.Clear();

        foreach (var binding in activePreset.Bindings)
        {
            commandBindings[binding.Command] = binding;
        }
    }
    
    private async Task HandleCommand(string command)
    {
        var test = command.Split(": ")[1].ToLower(); 
        if (!commandBindings.TryGetValue(command.Split(": ")[1].ToLower(), out var binding))
            return;

        switch (binding.Action)
        {
            case "HoldKey":
                await Task.Run(() => InputSimulator.HoldKey(binding.Key));
                break;
            case "ReleaseKeys":
                await Task.Run(() =>
                {
                    InputSimulator.ReleaseKey(binding.Key);
                    InputSimulator.ReleaseKey(KeyCodes.S);
                });
                break;
            case "HoldAndReleaseKey":
                await Task.Run(() => InputSimulator.HoldAndReleaseKey(binding.Key, binding.Duration));
                break;
            case "MouseClick":
                Task.Run(() =>
                {
                    InputSimulator.MouseLeftDown();
                    Thread.Sleep(1000);
                    InputSimulator.MouseLeftUp();
                });
                break;
            case "MoveMouse":
                Task.Run(() => InputSimulator.MoveMouse(binding.MouseMovement.X, binding.MouseMovement.Y));
                break;
        }
    }


    private void InitializeDefaultBindings()
    {
        // Clear the current configuration
        currentConfig = new BindingConfig();

        // Create default preset
        var defaultPreset = new KeyMapPreset { Name = "Default" };

        // Add bindings to the default preset
        defaultPreset.Bindings.AddRange(new[]
        {
            new KeyBinding { Command = "left", Action = "HoldKey", Key = KeyCodes.A, Duration = 2000 },
            new KeyBinding { Command = "right", Action = "HoldKey", Key = KeyCodes.D, Duration = 2000 },
            new KeyBinding { Command = "drive", Action = "HoldKey", Key = KeyCodes.W },
            new KeyBinding { Command = "stop", Action = "ReleaseKeys", Key = KeyCodes.W },
            new KeyBinding { Command = "brake", Action = "HoldAndReleaseKey", Key = KeyCodes.SPACE, Duration = 700 },
            new KeyBinding { Command = "shoot", Action = "MouseClick" },
            new KeyBinding { Command = "aim up", Action = "MoveMouse", MouseMovement = new Point(0, -30) },
            new KeyBinding { Command = "aim down", Action = "MoveMouse", MouseMovement = new Point(0, 30) },
            new KeyBinding { Command = "aim right", Action = "MoveMouse", MouseMovement = new Point(30, 0) },
            new KeyBinding { Command = "aim left", Action = "MoveMouse", MouseMovement = new Point(-30, 0) }
        });

        // Add preset to configuration
        currentConfig.Presets.Add(defaultPreset);

        // Update active preset and bindings
        activePreset = defaultPreset;
        UpdateCommandBindings();

        // Save the configuration
        SavePresets();
    }
}
